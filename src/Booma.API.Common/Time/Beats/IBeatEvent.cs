using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C5;

namespace Booma
{
	/// <summary>
	/// Contract for a Beat-based event.
	/// </summary>
	public interface IBeatEvent
	{
		/// <summary>
		/// The scheduled Beat time this event should be fired at.
		/// </summary>
		double ScheduledBeatTime { get; }

		/// <summary>
		/// Dispatches the event.
		/// </summary>
		void Dispatch();
	}
}
