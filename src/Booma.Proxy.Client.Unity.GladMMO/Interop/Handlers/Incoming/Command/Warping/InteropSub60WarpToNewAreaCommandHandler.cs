using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;
using UnityEngine;

namespace Booma.Proxy
{

	[PSOBBHandler]
	public sealed class InteropSub60WarpToNewAreaCommandHandler : InteropCommand60Handler<Sub60WarpToNewAreaCommand>
	{
		private IEntityGuidMappable<int, PlayerZoneData> ZoneDataMappable { get; }

		public InteropSub60WarpToNewAreaCommandHandler(ILog logger,
			[NotNull] IEntityGuidMappable<int, PlayerZoneData> zoneDataMappable) 
			: base(logger)
		{
			ZoneDataMappable = zoneDataMappable ?? throw new ArgumentNullException(nameof(zoneDataMappable));
		}

		protected override async Task HandleSubMessage(InteropPSOBBPeerMessageContext context, Sub60WarpToNewAreaCommand command)
		{
			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, (short)command.Identifier);
			ZoneDataMappable[entityGuid] = new PlayerZoneData(command.Zone);
		}
	}
}
