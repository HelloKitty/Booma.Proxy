using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Enumeration of registeration types for beat-based events.
	/// </summary>
	public enum BeatsEventRegisterationType
	{
		/// <summary>
		/// Event that is consumed one use.
		/// </summary>
		OneTime = 0,

		/// <summary>
		/// Event that isn't consumed and is reschedule when it is dispatched.
		/// </summary>
		Repeating = 1,
	}
}
