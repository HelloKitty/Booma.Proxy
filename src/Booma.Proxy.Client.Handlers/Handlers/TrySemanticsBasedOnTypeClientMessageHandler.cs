using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Handler that implements try semantics in attempting to handle a provided message.
	/// It can indicate if the message is consumed/consumable.
	/// </summary>
	/// <typeparam name="TPayloadBaseType"></typeparam>
	public sealed class TrySemanticsBasedOnTypeClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType, TPayloadType> : IClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType>
		where TIncomingPayloadType : class
		where TPayloadType : class, TIncomingPayloadType
		where TOutgoingPayloadType : class
	{
		/// <summary>
		/// Decorated payload handler that can handle
		/// payloads of type <typeparamref name="TPayloadType"/>.
		/// </summary>
		private IClientPayloadSpecificMessageHandler<TPayloadType, TOutgoingPayloadType> DecoratedPayloadHandler { get; }

		/// <inheritdoc />
		public TrySemanticsBasedOnTypeClientMessageHandler(IClientPayloadSpecificMessageHandler<TPayloadType, TOutgoingPayloadType> decoratedPayloadHandler)
		{
			if(decoratedPayloadHandler == null) throw new ArgumentNullException(nameof(decoratedPayloadHandler));

			DecoratedPayloadHandler = decoratedPayloadHandler;
		}

		/// <summary>
		/// Attempts to handle the provided <see cref="message"/> and will succeed if the
		/// payload is of type <typeparamref name="TPayloadType"/>.
		/// Otherwise will return false and not consume the message.
		/// </summary>
		/// <param name="context">The context of the message.</param>
		/// <param name="message">The message.</param>
		/// <returns>True if the message has been consumed.</returns>
		public async Task<bool> TryHandleMessage(IClientMessageContext<TOutgoingPayloadType> context, PSOBBNetworkIncomingMessage<TIncomingPayloadType> message)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));
			if(message == null) throw new ArgumentNullException(nameof(message));

			if(message.Payload is TPayloadType payload)
			{
				//No matter what happens in the handler we should indicate that it's consumed
				await DecoratedPayloadHandler.HandleMessage(context, payload);
				return true;
			}

			//Default semantics is a handler can only handle a specific type
			//So we just indicate that we can't handle the message and the caller
			//will hopefully find someone else to handle it.
			return false;
		}
	}
}
