using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Handler that can intercept payloads for completing async awaited tasks.
	/// </summary>
	/// <typeparam name="TIncomingPayloadType"></typeparam>
	/// <typeparam name="TOutgoingPayloadType"></typeparam>
	public sealed class InterceptAsyncRequestClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType> : IClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType>, IPayloadInterceptable
		where TIncomingPayloadType : class
		where TOutgoingPayloadType : class
	{
		/// <summary>
		/// The handler that is decorated
		/// </summary>
		private IClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType> DecoratedHandler { get; }

		/// <summary>
		/// Lock object.
		/// </summary>
		private readonly object SyncObj = new object();

		/// <summary>
		/// Map that allows registering short-circuit interceptions that will
		/// skip the handlers and complete tasks that will allow awaiting contexts for specific payloads
		/// to resume.
		/// </summary>
		private ConcurrentDictionary<Type, Queue<Action<object>>> ShortCircuitCompletionMap { get; }

		/// <inheritdoc />
		public InterceptAsyncRequestClientMessageHandler(IClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType> decoratedHandler)
		{
			if(decoratedHandler == null) throw new ArgumentNullException(nameof(decoratedHandler));

			DecoratedHandler = decoratedHandler;
			ShortCircuitCompletionMap = new ConcurrentDictionary<Type, Queue<Action<object>>>();
		}

		/// <inheritdoc />
		public async Task<bool> TryHandleMessage(IClientMessageContext<TOutgoingPayloadType> context, PSOBBNetworkIncomingMessage<TIncomingPayloadType> message)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));
			if(message == null) throw new ArgumentNullException(nameof(message));

			//Check if it contains the key first
			if(ShortCircuitCompletionMap.ContainsKey(message?.Payload?.GetType()))
			{
				//It could still be empty
				Action<object> shortCircuitAction = null;
				lock(SyncObj)
				{
					if(ShortCircuitCompletionMap[message.Payload.GetType()].Count != 0)
						shortCircuitAction = ShortCircuitCompletionMap[message.Payload.GetType()].Dequeue();
				}
				
				//Could be null; if not we need to shortcircuit the handlers instead
				if(shortCircuitAction != null)
				{
					shortCircuitAction(message.Payload); //send payload to be completed
					return true;
				}
			}

			//otherwise default to the decorated handler
			return await DecoratedHandler.TryHandleMessage(context, message);
		}

		/// <inheritdoc />
		public Task<TResponseType> InterceptPayload<TResponseType>() where TResponseType : IPacketPayload
		{
			//To allow us to catch the incoming payloads and redirect them to the awaiting context
			//we need to create a source we manually control and provide an awaitable to the caller
			//to wait for
			TaskCompletionSource<TResponseType> responseCompletionSource = new TaskCompletionSource<TResponseType>();

			//Now we need to register it in the short circuit route queue
			lock(SyncObj)
			{
				if(ShortCircuitCompletionMap.ContainsKey(typeof(TResponseType)))
					ShortCircuitCompletionMap[typeof(TResponseType)].Enqueue(o => responseCompletionSource.SetResult((TResponseType)o));
				else
				{
					ShortCircuitCompletionMap[typeof(TResponseType)] = new Queue<Action<object>>(5);
					ShortCircuitCompletionMap[typeof(TResponseType)].Enqueue(o => responseCompletionSource.SetResult((TResponseType)o));
				}
			}

			return responseCompletionSource.Task;
		}
	}
}
