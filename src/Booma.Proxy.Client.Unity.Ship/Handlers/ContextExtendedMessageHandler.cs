using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;

namespace Booma.Proxy
{
	/// <summary>
	/// A <see cref="GameMessageHandler{TPayloadType}"/> for payloads that are <see cref="IMessageContextIdentifiable"/>.
	/// Will provided the <see cref="INetworkPlayer"/> representation to the handler method. Will not dispatch to the child handler
	/// if the client ID is unknown.
	/// </summary>
	/// <typeparam name="TPayloadType">The payload type to handle.</typeparam>
	/// <typeparam name="TContextType">The type of the context that is required to handle the payload.</typeparam>
	[Injectee]
	public abstract class ContextExtendedMessageHandler<TPayloadType, TContextType> : GameMessageHandler<TPayloadType>
		where TContextType : INetworkMessageContext 
		where TPayloadType : PSOBBGamePacketPayloadServer, IMessageContextIdentifiable
	{
		/// <summary>
		/// The context factory required to build the context for the message.
		/// </summary>
		[Inject]
		private INetworkMessageContextFactory<IMessageContextIdentifiable, TContextType> ContextFactory { get; }

		/// <inheritdoc />
		public override async Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, TPayloadType payload)
		{
			//Build the context first and check its validity
			TContextType payloadContext = ContextFactory.Create(payload);

			//Not sure if it's possible to encounter this but we should check to be sure
			if(!payloadContext.isValid)
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Recieved Code: {payload.OpCodeHexString()} {this.MessageName()} for unknown Id: {payload.Identifier}");

				return;
			}

			//Just dispatch with the newly constructed payload context
			await HandleMessage(context, payload, payloadContext);
		}

		/// <summary>
		/// Handler methods with the network context, the payload, and the payload context <typeparamref name="TContextType"/>.
		/// </summary>
		/// <param name="context">The network context.</param>
		/// <param name="payload">The payload to be handled.</param>
		/// <param name="payloadContext">The context for the payload.</param>
		/// <returns>An awaitable that completes when the handling is finished.</returns>
		protected abstract Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, TPayloadType payload, TContextType payloadContext);
	}
}
