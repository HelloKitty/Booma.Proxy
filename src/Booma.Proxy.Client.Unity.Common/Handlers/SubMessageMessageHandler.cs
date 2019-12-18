using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	/// <summary>
	/// Implementation of the <see cref="BaseUnityMessageHandler{TIncomingPayloadBaseType,TOutgoingPayloadType,TPayloadType}"/>
	/// that handles specifically <typeparamref name="TPayloadType"/> and dispatches read <typeparamref name="TSubMessageType"/> message.
	/// to the implementer of this type.
	/// </summary>
	/// <typeparam name="TSubMessageType">The inner message type to be handled.</typeparam>
	/// <typeparam name="TPayloadType">The payload type this submessage is in.</typeparam>
	public abstract class SubMessageMessageHandler<TSubMessageType, TPayloadType> : BaseUnityMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, TPayloadType>, ISubMessageHandler<TSubMessageType, TPayloadType>
		where TPayloadType : PSOBBGamePacketPayloadServer
	{
		/// <inheritdoc />
		public override bool CanHandle(NetworkIncomingMessage<PSOBBGamePacketPayloadServer> message)
		{
			//We have to overide this because base doesn't implement what we want
			if(message.Payload is TPayloadType p)
				return CheckIsHandlable(p);

			return false;
		}

		/// <summary>
		/// Should be implemented to add additional checks if the payload is handeable.
		/// </summary>
		/// <param name="payload"></param>
		/// <returns></returns>
		protected abstract bool CheckIsHandlable(TPayloadType payload);

		/// <summary>
		/// Should be implemented to retrieve the <typeparamref name="TSubMessageType"/>
		/// </summary>
		/// <param name="payload"></param>
		/// <returns></returns>
		protected abstract TSubMessageType RetrieveSubMessage(TPayloadType payload);

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, TPayloadType payload)
		{
			//We want to grab the subtype, but we don't know how. Let the implementers do it.
			//then we can dispatch it.
			await HandleSubMessage(context, RetrieveSubMessage(payload))
				.ConfigureAwait(true);
		}

		/// <summary>
		/// Implemented submessage handler for the specified <typeparamref name="TSubMessageType"/>.
		/// </summary>
		/// <param name="context">The context of the submessage.</param>
		/// <param name="command">The submessage sent.</param>
		/// <returns>An awaitable that completes when the command handling is finished.</returns>
		protected abstract Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, TSubMessageType command);

		/// <inheritdoc />
		public override string ToString()
		{
			return $"SubmessageHandler: {GetType().Name} for SubMessage: {typeof(TSubMessageType).Name} Payload: {typeof(TPayloadType).Name}";
		}

		/// <inheritdoc />
		protected SubMessageMessageHandler(ILog logger) 
			: base(logger)
		{
			
		}

		/// <summary>
		/// Gets the loggable message name that the handler handles.
		/// </summary>
		/// <returns>The string name of the message type.</returns>
		public override string MessageName()
		{
			return typeof(TSubMessageType).Name;
		}
	}
}
