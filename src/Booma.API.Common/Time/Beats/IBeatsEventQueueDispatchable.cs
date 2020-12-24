using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for a type that contains dispatchables for
	/// a Beats based event.
	/// </summary>
	public interface IBeatsEventQueueDispatchable
	{
		/// <summary>
		/// Indicates if there is a scheduled Beat event ready and
		/// waiting to be dispatched.
		/// </summary>
		bool isBeatEventReady { get; }

		/// <summary>
		/// Dispatches the available Beats event.
		/// (Throws if <see cref="isBeatEventReady"/> is false)
		/// </summary>
		/// <exception cref="InvalidOperationException">Throws if <see cref="isBeatEventReady"/> is false.</exception>
		void DispatchNext();
	}
}
