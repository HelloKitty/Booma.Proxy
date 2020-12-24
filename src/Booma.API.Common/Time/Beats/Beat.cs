using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Static methods for Beats time.
	/// </summary>
	public static class Beat
	{
		/// <summary>
		/// Creates the <see cref="double"/> equivalent of the
		/// provided <see cref="beatCount"/> in beats.
		/// </summary>
		/// <param name="beatCount">The count of the beats.</param>
		/// <returns>The beat representation of <see cref="beatCount"/> many beats.</returns>
		public static double Beats(int beatCount)
		{
			//It's just the count
			return beatCount;
		}

		/// <summary>
		/// Creates the <see cref="double"/> equivalent of the
		/// provided <see cref="beatCount"/> in centibeats.
		/// </summary>
		/// <param name="beatCount">The count of the centibeats.</param>
		/// <returns>The beat representation of <see cref="beatCount"/> many centibeats.</returns>
		public static double CentiBeats(int beatCount)
		{
			return beatCount / 100d;
		}

		/// <summary>
		/// Creates the <see cref="double"/> equivalent of the
		/// provided <see cref="beatCount"/> in MilliBeats.
		/// </summary>
		/// <param name="beatCount">The count of the millibeats.</param>
		/// <returns>The beat representation of <see cref="beatCount"/> many millibeats.</returns>
		public static double MilliBeats(int beatCount)
		{
			return beatCount / 1000d;
		}
	}
}
