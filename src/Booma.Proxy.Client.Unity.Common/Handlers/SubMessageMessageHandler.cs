using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Implementation of the <see cref="BaseUnityMessageHandler{TIncomingPayloadBaseType,TOutgoingPayloadType,TPayloadType}"/>
	/// that handles specifically <typeparamref name="TPayloadType"/> and dispatches read <typeparamref name="TSubMessageType"/> message.
	/// to the implementer of this type.
	/// </summary>
	/// <typeparam name="TSubMessageType">The inner message type to be handled.</typeparam>
	/// <typeparam name="TPayloadType">The payload type this submessage is in.</typeparam>
	public abstract class SubMessageMessageHandler<TSubMessageType, TPayloadType> : BaseUnityMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, TPayloadType> 
		where TPayloadType : PSOBBGamePacketPayloadServer
	{
		/// <inheritdoc />
		public override async Task<bool> TryHandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, PSOBBNetworkIncomingMessage<PSOBBGamePacketPayloadServer> message)
		{
			//Odd design but we override so we can check that this is the payload
			//and then check if we have the right command type
			if(message.Payload is TPayloadType payload)
				if(CheckIsHandlable(payload))
					return await base.TryHandleMessage(context, message);

			//If it's not then we don't want to consume it
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
		public override async Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, TPayloadType payload)
		{
			//We want to grab the subtype, but we don't know how. Let the implementers do it.
			//then we can dispatch it.
			await HandleSubMessage(context, RetrieveSubMessage(payload));
		}

		/// <summary>
		/// Implemented submessage handler for the specified <typeparamref name="TSubMessageType"/>.
		/// </summary>
		/// <param name="context">The context of the submessage.</param>
		/// <param name="command">The submessage sent.</param>
		/// <returns>An awaitable that completes when the command handling is finished.</returns>
		protected abstract Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, TSubMessageType command);
	}
}
