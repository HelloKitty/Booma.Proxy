using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Extensions for the various interfaces/types involved in Beats events.
	/// </summary>
	public static class BeatsEventQueueExtensions
	{
		/// <summary>
		/// Registers a new event "<see cref="action"/>" that occurs on the next beat.
		/// </summary>
		/// <param name="beatQueue">The beat queue.</param>
		/// <param name="action">The action to register.</param>
		public static void RegisterOnNextBeat(this IBeatsEventQueueRegisterable beatQueue, Action action)
		{
			if(action == null) throw new ArgumentNullException(nameof(action));

			//Registers the event to happen on the next beat.
			beatQueue.RegisterEvent(TimeService.CurrentBeatsTime % 1d, action);
		}

		/// <summary>
		/// Registers a new event "<see cref="action"/>" that occurs on the next centibeat. 1/100 of a Beat
		/// </summary>
		/// <param name="beatQueue">The beat queue.</param>
		/// <param name="action">The action to register.</param>
		public static void RegisterOnNextCentiBeat(this IBeatsEventQueueRegisterable beatQueue, Action action)
		{
			if(action == null) throw new ArgumentNullException(nameof(action));

			//This should basically fire instantly
			//Registers the event to happen on the next beat.
			beatQueue.RegisterEvent(TimeService.CurrentBeatsTime % 0.01d, action);
		}

		/// <summary>
		/// Registers a new event "<see cref="action"/>" that occurs in <see cref="beatCount"/> amount of beats.
		/// </summary>
		/// <param name="beatQueue">The beat queue.</param>
		/// <param name="action">The action to register.</param>
		/// <param name="beatCount">The amount of beats that should pass before this event is dispatched</param>
		public static void RegisterBeatsFromNow(this IBeatsEventQueueRegisterable beatQueue, Action action, int beatCount)
		{
			if(beatCount < 0) throw new ArgumentOutOfRangeException(nameof(beatCount), "Registered beat time for event cannot be less than current time.");
			if(action == null) throw new ArgumentNullException(nameof(action));

			beatQueue.RegisterEvent(TimeService.CurrentBeatsTime + (double)beatCount, action);
		}

		/// <summary>
		/// Registers a new event "<see cref="action"/>" that occurs in <see cref="centiBeatCount"/> amount of centibeats.
		/// </summary>
		/// <param name="beatQueue">The beat queue.</param>
		/// <param name="action">The action to register.</param>
		/// <param name="centiBeatCount">The amount of centibeats that should pass before this event is dispatched</param>
		public static void RegisterCentiBeatsFromNow(this IBeatsEventQueueRegisterable beatQueue, Action action, int centiBeatCount)
		{
			if(centiBeatCount < 0) throw new ArgumentOutOfRangeException(nameof(centiBeatCount), "Registered beat time for event cannot be less than current time.");
			if(action == null) throw new ArgumentNullException(nameof(action));

			beatQueue.RegisterEvent(TimeService.CurrentBeatsTime + (centiBeatCount / 100d), action);
		}

		/// <summary>
		/// Registers a new repeating Beat event that will re-register before firing and will register itself for the
		/// difference between the schedule time and the current time.
		/// </summary>
		/// <param name="beatQueue">The beat queue.</param>
		/// <param name="action">The The action to register.</param>
		/// <param name="repeatBeatInterval">The beat interval to repeat the event for.</param>
		public static void RegisterRepeating(this IBeatsEventQueueRegisterable beatQueue, Action action, double repeatBeatInterval)
		{
			//Register an event that reschedules itself before being fired
			beatQueue.RegisterEvent(TimeService.CurrentBeatsTime + repeatBeatInterval, () => ReschedulingEvent(action, beatQueue, repeatBeatInterval));
		}

		/// <summary>
		/// Decorates an action with re-registeration to the queue based on the provided Beat <see cref="interval"/> value.
		/// </summary>
		/// <param name="action">The action to decorate with rescheduling.</param>
		/// <param name="beatQueue">The beat queue to register it to.</param>
		/// <param name="interval">The internal of registeration.</param>
		/// <returns>A new decorated action with rescheduling semantics.</returns>
		private static Action ReschedulingEvent(Action action, IBeatsEventQueueRegisterable beatQueue, double interval)
		{
			return () =>
			{
				beatQueue.RegisterEvent(TimeService.CurrentBeatsTime + interval, action);
				action();
			};
		}
	}
}
