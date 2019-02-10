using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	//We handle this abit differently for games.
	//Unlike lobby, this doesn't mean we should set new zone for them.
	//It only means that we should set the new zone data IF the client is bursting.
	//If client is bursting, then we won't get zone data any other way so we NEED to store it if the particular client is bursting.
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	public sealed class GameSub60WarpToNewAreaCommandHandler : Command60Handler<Sub60WarpToNewAreaCommand>
	{
		private IEntityGuidMappable<PlayerZoneData> ZoneDataMappable { get; }

		private IBurstingService BurstingService { get; }

		/// <inheritdoc />
		public GameSub60WarpToNewAreaCommandHandler(ILog logger, [NotNull] IEntityGuidMappable<PlayerZoneData> zoneDataMappable, [NotNull] IBurstingService burstingService) 
			: base(logger)
		{
			ZoneDataMappable = zoneDataMappable ?? throw new ArgumentNullException(nameof(zoneDataMappable));
			BurstingService = burstingService ?? throw new ArgumentNullException(nameof(burstingService));
		}

		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60WarpToNewAreaCommand command)
		{
			//For games, we only set zone data in this handler if they are bursting right now.
			if(BurstingService.isBurstingInProgress && BurstingService.BurstingEntityGuid.Value == EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier))
			{
				ZoneDataMappable[EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier)] = new PlayerZoneData(command.Zone);
			}
			
			return Task.CompletedTask;
		}
	}
}
