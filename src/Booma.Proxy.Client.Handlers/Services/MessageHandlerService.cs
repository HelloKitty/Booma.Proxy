using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Message handling service that aggregates handlers
	/// and manages the try dispatching of them and accepts a default
	/// handler to fall back to if it fails.
	/// </summary>
	/// <typeparam name="TIncomingPayloadType"></typeparam>
	/// <typeparam name="TOutgoingPayloadType"></typeparam>
	public sealed class MessageHandlerService<TIncomingPayloadType, TOutgoingPayloadType> : IClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType> 
		where TIncomingPayloadType : class 
		where TOutgoingPayloadType : class
	{
		/// <summary>
		/// The handlers this service will try to dispatch to.
		/// </summary>
		private IEnumerable<IClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType>> ManagedHandlers { get; }

		/// <summary>
		/// The optional default message handler to fall back on
		/// if no handler accepts the incoming message.
		/// </summary>
		private IClientPayloadSpecificMessageHandler<TIncomingPayloadType, TOutgoingPayloadType> DefaultMessageHandler { get; }

		/// <inheritdoc />
		public MessageHandlerService(IEnumerable<IClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType>> managedHandlers, IClientPayloadSpecificMessageHandler<TIncomingPayloadType, TOutgoingPayloadType> defaultMessageHandler)
		{
			if(managedHandlers == null) throw new ArgumentNullException(nameof(managedHandlers));

			ManagedHandlers = managedHandlers;

			//Default handler can be null.
			DefaultMessageHandler = defaultMessageHandler;
		}

		/// <inheritdoc />
		public bool CanHandle(PSOBBNetworkIncomingMessage<TIncomingPayloadType> message)
		{
			//We can always handle messages
			return true;
		}

		/// <inheritdoc />
		public async Task<bool> TryHandleMessage(IClientMessageContext<TOutgoingPayloadType> context, PSOBBNetworkIncomingMessage<TIncomingPayloadType> message)
		{
			//TODO: What should we do about exceptions?
			//When a message comes in we need to try to dispatch it to all handlers
			foreach(IClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType> handler in ManagedHandlers)
			{
				//If we found a handler that handled it we should stop trying to handle it and return true
				if(handler.CanHandle(message))
					return await handler.TryHandleMessage(context, message);
			}

			DefaultMessageHandler?.HandleMessage(context, message.Payload)?
				.ConfigureAwait(true);

			return false;
		}
	}
}
