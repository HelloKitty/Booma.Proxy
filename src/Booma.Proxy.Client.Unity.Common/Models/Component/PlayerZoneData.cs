using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Data model that contains information about the zone
	/// a player is in.
	/// </summary>
	[NonTransientEntityData]
	public sealed class PlayerZoneData
	{
		/// <summary>
		/// The ID of the zone the player is in.
		/// </summary>
		public int ZoneId { get; }

		/// <inheritdoc />
		public PlayerZoneData(int zoneId)
		{
			ZoneId = zoneId;
		}
	}
}
