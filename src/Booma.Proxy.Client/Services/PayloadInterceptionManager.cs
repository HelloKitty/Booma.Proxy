using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/// <summary>
	/// Aids in intercepting payloads for completing async awaited tasks.
	/// </summary>
	/// <typeparam name="TIncomingPayloadType"></typeparam>
	public sealed class PayloadInterceptionManager<TIncomingPayloadType> : IPayloadInterceptable
		where TIncomingPayloadType : class
	{
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
		public PayloadInterceptionManager()
		{
			ShortCircuitCompletionMap = new ConcurrentDictionary<Type, Queue<Action<object>>>();
		}

		/// <summary>
		/// Indicates if a payload is being intercepted.
		/// If it's not then handling should be done normally but if it's intercepted
		/// do not interact with the payload after this call returns.
		/// </summary>
		/// <param name="payload"></param>
		/// <returns></returns>
		public bool TryNotifyOutstandingInterceptors([NotNull] TIncomingPayloadType payload)
		{
			if(payload == null) throw new ArgumentNullException(nameof(payload));

			//Check if it contains the key first
			if(ShortCircuitCompletionMap.ContainsKey(payload.GetType()))
			{
				//It could still be empty
				Action<object> shortCircuitAction = null;
				lock(SyncObj)
				{
					if(ShortCircuitCompletionMap[payload.GetType()].Count != 0)
						shortCircuitAction = ShortCircuitCompletionMap[payload.GetType()].Dequeue();
				}

				//Could be null; if not we need to shortcircuit the handlers instead
				if(shortCircuitAction != null)
				{
					shortCircuitAction(payload); //send payload to be completed
					return true;
				}
			}

			return false;
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
