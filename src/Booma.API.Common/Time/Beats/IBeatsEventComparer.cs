using System.Collections.Generic;

namespace Booma
{
	public sealed class IBeatsEventComparer : IComparer<IBeatEvent>
	{
		/// <summary>
		/// Compares the scheduled times.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public int Compare(IBeatEvent x, IBeatEvent y)
		{
			//COmpare scheduled times to find which is higher.
			if(x.ScheduledBeatTime > y.ScheduledBeatTime)
				return 1;
			else if(x.ScheduledBeatTime < y.ScheduledBeatTime)
				return -1;

			return 0;
		}
	}
}