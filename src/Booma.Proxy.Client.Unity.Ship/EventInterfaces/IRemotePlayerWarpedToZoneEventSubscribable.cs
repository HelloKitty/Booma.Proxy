using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IRemotePlayerWarpedToZoneEventSubscribable
	{
		event EventHandler<PlayerWarpedToZoneEventArgs> OnRemotePlayedFinishedWarpToZone;
	}

	public sealed class PlayerWarpedToZoneEventArgs : EventArgs, IEntityIdentifable
	{
		public int EntityGuid { get; }

		public int ZoneId { get; }

		/// <inheritdoc />
		public PlayerWarpedToZoneEventArgs(int entityGuid, int zoneId)
		{
			if(zoneId < 0) throw new ArgumentOutOfRangeException(nameof(zoneId));

			EntityGuid = entityGuid;
			ZoneId = zoneId;
		}
	}
}
