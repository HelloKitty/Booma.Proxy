using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public abstract class SharedBaseSingleEventListenerInitializable<TSubscribableType, THandlerType, TEventArgsType> : IGameInitializable
		where TSubscribableType : class
		where THandlerType : Delegate
		where TEventArgsType : EventArgs
	{
		private object SyncObj = new object();

		/// <summary>
		/// The cached efficient delegate pointing to the Add method of an Event for registering a handler.
		/// </summary>
		private static Action<TSubscribableType, THandlerType> CachedEventRegisterationDelegate { get; }

		/// <summary>
		/// The cached efficient delegate pointing to the Add method of an Event for registering a handler.
		/// </summary>
		private static Action<TSubscribableType, THandlerType> CachedEventRemoveDelegate { get; }

		/// <summary>
		/// Subscription service containing a <see cref="EventHandler"/>.
		/// </summary>
		protected internal TSubscribableType SubscriptionService { get; }

		/// <summary>
		/// Indicates if the listener is subscribed to the <see cref="SubscriptionService"/>.
		/// </summary>
		protected bool isSubscribed { get; private set; }

		/// <inheritdoc />
		internal SharedBaseSingleEventListenerInitializable([NotNull] TSubscribableType subscriptionService)
		{
			SubscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
		}

		static SharedBaseSingleEventListenerInitializable()
		{
			EventInfo[] events = typeof(TSubscribableType)
				.GetEvents(BindingFlags.Public | BindingFlags.Instance);

			events = events
				.Where(e => IsCorrectEventSignature(e))
				.ToArray();

			if(events.Length != 1)
				throw new InvalidOperationException($"Cannot specify: {typeof(TSubscribableType).Name} as SingleEvent with Args: {typeof(EventArgs)} because: {ComputeErrorMessage(events)}");

			//If we've made it here, there is ONE event in the collection
			//and it fits the requirements
			CachedEventRegisterationDelegate = (Action<TSubscribableType, THandlerType>)events[0]
				.AddMethod.CreateDelegate(typeof(Action<TSubscribableType, THandlerType>));

			CachedEventRemoveDelegate = (Action<TSubscribableType, THandlerType>)events[0]
				.RemoveMethod.CreateDelegate(typeof(Action<TSubscribableType, THandlerType>));
		}

		private static bool IsCorrectEventSignature(EventInfo e)
		{
			return e.EventHandlerType == typeof(THandlerType);
		}

		private static string ComputeErrorMessage(EventInfo[] events)
		{
			return events.Length > 1 ? "Multiple events have the same Type signature" : "No event matches the type signature";
		}

		/// <summary>
		/// Called when the subscription service fires an event.
		/// </summary>
		/// <param name="source">The calling source.</param>
		/// <param name="args"></param>
		protected abstract void OnEventFired(object source, TEventArgsType args);

		//TODO: Doc exceptions/warnings.
		/// <summary>
		/// Unregisters the event handler <see cref="OnEventFired"/> from the
		/// <see cref="SubscriptionService"/>.
		/// </summary>
		protected void Unsubscribe()
		{
			lock(SyncObj)
			{
				if(!isSubscribed)
					throw new InvalidOperationException($"Cannot {nameof(Unsubscribe)} in {GetType().Name} without already being subscribed.");

				HandleOnEventFiredCast(CachedEventRemoveDelegate);
				isSubscribed = false;
			}
		}

		//TODO: Doc exceptions/warnings.
		/// <summary>
		/// Registers the event handler <see cref="OnEventFired"/> to the
		/// <see cref="SubscriptionService"/>.
		/// </summary>
		protected void Subscribe()
		{
			lock(SyncObj)
			{
				if(isSubscribed)
					throw new InvalidOperationException($"Cannot {nameof(Subscribe)} multiple times in {GetType().Name}. Subscriptions should only occur once..");

				HandleOnEventFiredCast(CachedEventRegisterationDelegate);
				isSubscribed = true;
			}
		}

		/// <inheritdoc />
		public virtual Task OnGameInitialized()
		{
			//TODO: Is it suppose to actually be the SubService as the registeration arg??
			Subscribe();
			return Task.CompletedTask;
		}

		protected internal abstract void HandleOnEventFiredCast(Action<TSubscribableType, THandlerType> targetSubscriptionMethod);
	}

	/// <summary>
	/// Base type for a Single event listening <see cref="IGameInitializable"/>.
	/// Will register a callback <see cref="OnEventFired"/> to the event on <see cref="TSubscribableType"/>
	/// that has an event signature with either EventArgs or <see cref="EventHandler"/>.
	/// </summary>
	/// <typeparam name="TSubscribableType">The subscription interface.</typeparam>
	public abstract class BaseSingleEventListenerInitializable<TSubscribableType> : SharedBaseSingleEventListenerInitializable<TSubscribableType, EventHandler, EventArgs>
		where TSubscribableType : class
	{
		/// <inheritdoc />
		protected BaseSingleEventListenerInitializable(TSubscribableType subscriptionService)
			: base(subscriptionService)
		{

		}

		/// <inheritdoc />
		protected internal override void HandleOnEventFiredCast(Action<TSubscribableType, EventHandler> targetSubscriptionMethod)
		{
			targetSubscriptionMethod.Invoke(SubscriptionService, OnEventFired);
		}
	}

	/// <summary>
	/// Base type for a Single event listening <see cref="IGameInitializable"/>.
	/// Will register a callback <see cref="OnEventFired"/> to the event on <see cref="TSubscribableType"/>
	/// that has an event signature with args <see cref="TEventHandlerArgsType"/>.
	/// </summary>
	/// <typeparam name="TSubscribableType">The subscription interface.</typeparam>
	/// <typeparam name="TEventHandlerArgsType">The type of args the event publishes.</typeparam>
	public abstract class BaseSingleEventListenerInitializable<TSubscribableType, TEventHandlerArgsType> : SharedBaseSingleEventListenerInitializable<TSubscribableType, EventHandler<TEventHandlerArgsType>, TEventHandlerArgsType>
		where TSubscribableType : class 
		where TEventHandlerArgsType : EventArgs
	{
		/// <inheritdoc />
		protected BaseSingleEventListenerInitializable(TSubscribableType subscriptionService) 
			: base(subscriptionService)
		{

		}

		/// <inheritdoc />
		protected internal override void HandleOnEventFiredCast(Action<TSubscribableType, EventHandler<TEventHandlerArgsType>> targetSubscriptionMethod)
		{
			targetSubscriptionMethod.Invoke(SubscriptionService, OnEventFired);
		}
	}
}
