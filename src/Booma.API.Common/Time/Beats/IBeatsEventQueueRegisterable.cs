using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	//This is the simpliest implementation however there are extension methods. You do not need to use the method directly.
	/// <summary>
	/// Contract for an event queue based on Beat time.
	/// </summary>
	public interface IBeatsEventQueueRegisterable
	{
		/// <summary>
		/// Registers a new Beats event <see cref="eventToDispatch"/> to be dispatched at the specified
		/// Beats time <see cref="scheduledDispatchBeatTime"/>.
		/// </summary>
		/// <param name="scheduledDispatchBeatTime">The time to dispatch the event.</param>
		/// <param name="eventToDispatch">The event to dispatch.</param>
		void RegisterEvent(double scheduledDispatchBeatTime, Action eventToDispatch);
	}
}
