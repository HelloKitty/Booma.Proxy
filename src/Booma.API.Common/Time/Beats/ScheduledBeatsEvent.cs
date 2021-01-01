using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C5;

namespace Booma
{
	//TODO: For efficiency we could make these reusbale for repeating
	/// <summary>
	/// Model for a scheduled Beats event.
	/// </summary>
	public sealed class ScheduledBeatsEvent : IBeatEvent
	{
		/// <summary>
		/// The scheduled Beat time this event should be fired at.
		/// </summary>
		public double ScheduledBeatTime { get; }

		/// <summary>
		/// The scheduled event to dispatch.
		/// </summary>
		private Action EventAction { get; }

		/// <inheritdoc />
		public ScheduledBeatsEvent(double scheduledBeatTime, Action eventAction)
		{
			if(eventAction == null) throw new ArgumentNullException(nameof(eventAction));

			ScheduledBeatTime = scheduledBeatTime;
			EventAction = eventAction;
		}

		/// <inheritdoc />
		public void Dispatch()
		{
			//Just dispatch the event and do nothing to the queue
			EventAction();
		}
	}
}
