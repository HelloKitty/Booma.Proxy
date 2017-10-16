using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Enumeration of scale flags.
	/// These can indicate what to scale or not scale.
	/// </summary>
	[Flags]
	public enum UnitScaleFlags : int
	{
		/// <summary>
		/// Represents no dimensions.
		/// </summary>
		None = 0,

		/// <summary>
		/// The x dimension.
		/// </summary>
		X = 1 << 0,

		/// <summary>
		/// The y dimension.
		/// </summary>
		Y = 1 << 1,

		/// <summary>
		/// The z dimension.
		/// </summary>
		Z = 1 << 2,

		/// <summary>
		/// The w dimension.
		/// </summary>
		W = 1 << 3
	}
}
