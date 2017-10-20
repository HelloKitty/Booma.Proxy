using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Static time functions and features.
	/// </summary>
	public static class TimeService
	{
		private static double _startBeatsTime;
		private static float unitySecondsStartTime;

		private static readonly object syncObj = new object();

		/// <summary>
		/// The starting beats time.
		/// </summary>
		public static double StartBeatsTime
		{
			get
			{
				lock(syncObj)
					return _startBeatsTime;
			}

			set
			{
				lock(syncObj)
				{
					unitySecondsStartTime = UnityEngine.Time.realtimeSinceStartup;
					_startBeatsTime = value;
				}
			}
		}

		//See: https://en.wikipedia.org/wiki/Swatch_Internet_Time#Calculation_from_UTC.2B1
		/// <summary>
		/// The current time in Beats.
		/// (The start time + time since)
		/// </summary>
		public static double CurrentBeatsTime => StartBeatsTime + ((UnityEngine.Time.realtimeSinceStartup - unitySecondsStartTime) / 86.4f);
	}
}
