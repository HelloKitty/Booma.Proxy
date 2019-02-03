using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Base type for a Single event listening <see cref="IGameInitializable"/>.
	/// Will register a callback <see cref="OnEventFired"/> to the event on <see cref="TSubscribableType"/>
	/// that has an event signature with either EventArgs or <see cref="EventHandler"/>.
	/// </summary>
	/// <typeparam name="TSubscribableType">The subscription interface.</typeparam>
	public abstract class BaseSingleEventListenerInitializable<TSubscribableType> : IGameInitializable
		where TSubscribableType : class
	{
		/// <summary>
		/// The cached efficient delegate pointing to the Add method of an Event for registering a handler.
		/// </summary>
		private static Action<TSubscribableType, EventHandler> CachedEventRegisterationDelegate { get; }

		/// <summary>
		/// The cached efficient delegate pointing to the Add method of an Event for registering a handler.
		/// </summary>
		private static Action<TSubscribableType, EventHandler> CachedEventRemoveDelegate { get; }

		/// <summary>
		/// Subscription service containing a <see cref="EventHandler"/>.
		/// </summary>
		private TSubscribableType SubscriptionService { get; }

		static BaseSingleEventListenerInitializable()
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
			CachedEventRegisterationDelegate = (Action<TSubscribableType, EventHandler>)events[0]
				.AddMethod.CreateDelegate(typeof(Action<TSubscribableType, EventHandler>));

			CachedEventRemoveDelegate = (Action<TSubscribableType, EventHandler>)events[0]
				.RemoveMethod.CreateDelegate(typeof(Action<TSubscribableType, EventHandler>));
		}

		private static string ComputeErrorMessage(EventInfo[] events)
		{
			return events.Length > 1 ? "Multiple events have the same Type signature" : "No event matches the type signature";
		}

		private static bool IsCorrectEventSignature(EventInfo e)
		{
			//We need special heanling for EventArgs or EventHandler non-generic
			return e.EventHandlerType == typeof(EventHandler);
		}

		/// <inheritdoc />
		protected BaseSingleEventListenerInitializable([NotNull] TSubscribableType subscriptionService)
		{
			SubscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
		}

		/// <summary>
		/// Called when the subscription service fires an event.
		/// </summary>
		/// <param name="source">The calling source.</param>
		/// <param name="args"></param>
		protected abstract void OnEventFired(object source, EventArgs args);

		/// <summary>
		/// Unregistes the event handler <see cref="OnEventFired"/> from the
		/// <see cref="SubscriptionService"/>.
		/// </summary>
		protected void Unsubscribe()
		{
			CachedEventRemoveDelegate.Invoke(SubscriptionService, OnEventFired);
		}

		/// <inheritdoc />
		public virtual Task OnGameInitialized()
		{
			//TODO: Is it suppose to actually be the SubService as the registeration arg??
			CachedEventRegisterationDelegate(SubscriptionService, OnEventFired);
			return Task.CompletedTask;
		}
	}

	/// <summary>
	/// Base type for a Single event listening <see cref="IGameInitializable"/>.
	/// Will register a callback <see cref="OnEventFired"/> to the event on <see cref="TSubscribableType"/>
	/// that has an event signature with args <see cref="TEventHandlerArgsType"/>.
	/// </summary>
	/// <typeparam name="TSubscribableType">The subscription interface.</typeparam>
	/// <typeparam name="TEventHandlerArgsType">The type of args the event publishes.</typeparam>
	public abstract class BaseSingleEventListenerInitializable<TSubscribableType, TEventHandlerArgsType> : IGameInitializable
		where TSubscribableType : class
	{
		/// <summary>
		/// The cached efficient delegate pointing to the Add method of an Event for registering a handler.
		/// </summary>
		private static Action<TSubscribableType, EventHandler<TEventHandlerArgsType>> CachedEventRegisterationDelegate { get; }

		/// <summary>
		/// The cached efficient delegate pointing to the Add method of an Event for registering a handler.
		/// </summary>
		private static Action<TSubscribableType, EventHandler<TEventHandlerArgsType>> CachedEventRemoveDelegate { get; }

		/// <summary>
		/// Subscription service containing a <typeparamref name="TEventHandlerArgsType"/> <see cref="EventHandler{T}"/>.
		/// </summary>
		private TSubscribableType SubscriptionService { get; }

		static BaseSingleEventListenerInitializable()
		{
			EventInfo[] events = typeof(TSubscribableType)
				.GetEvents(BindingFlags.Public | BindingFlags.Instance);

			events = events
				.Where(e => IsCorrectEventSignature(e))
				.ToArray();

			if(events.Length != 1)
				throw new InvalidOperationException($"Cannot specify: {typeof(TSubscribableType).Name} as SingleEvent with Args: {typeof(TEventHandlerArgsType)} because: {ComputeErrorMessage(events)}");

			//If we've made it here, there is ONE event in the collection
			//and it fits the requirements
			CachedEventRegisterationDelegate = (Action<TSubscribableType, EventHandler<TEventHandlerArgsType>>)events[0]
				.AddMethod.CreateDelegate(typeof(Action<TSubscribableType, EventHandler<TEventHandlerArgsType>>));

			CachedEventRemoveDelegate = (Action<TSubscribableType, EventHandler<TEventHandlerArgsType>>)events[0]
				.RemoveMethod.CreateDelegate(typeof(Action<TSubscribableType, EventHandler<TEventHandlerArgsType>>));
		}

		private static bool IsCorrectEventSignature(EventInfo e)
		{
			//We need special heanling for EventArgs or EventHandler non-generic
			return typeof(TEventHandlerArgsType) == typeof(EventArgs) ? 
				e.EventHandlerType == typeof(EventHandler) || e.EventHandlerType == typeof(EventHandler<TEventHandlerArgsType>) 
				: e.EventHandlerType == typeof(EventHandler<TEventHandlerArgsType>);
		}

		private static string ComputeErrorMessage(EventInfo[] events)
		{
			return events.Length > 1 ? "Multiple events have the same Type signature" : "No event matches the type signature";
		}

		/// <inheritdoc />
		protected BaseSingleEventListenerInitializable([NotNull] TSubscribableType subscriptionService)
		{
			SubscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
		}

		/// <summary>
		/// Called when the subscription service fires an event.
		/// </summary>
		/// <param name="source">The calling source.</param>
		/// <param name="args"></param>
		protected abstract void OnEventFired(object source, TEventHandlerArgsType args);

		/// <summary>
		/// Unregistes the event handler <see cref="OnEventFired"/> from the
		/// <see cref="SubscriptionService"/>.
		/// </summary>
		protected void Unsubscribe()
		{
			CachedEventRemoveDelegate.Invoke(SubscriptionService, OnEventFired);
		}

		/// <inheritdoc />
		public virtual Task OnGameInitialized()
		{
			//TODO: Is it suppose to actually be the SubService as the registeration arg??
			CachedEventRegisterationDelegate(SubscriptionService, OnEventFired);
			return Task.CompletedTask;
		}
	}
}
