using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	//What this means in the lobby is they are likely joining the lobby. If this is used ANYTIME else I think it
	//means a client is cheating and force teleporting to other/areas and maps that they shouldn't be going into
	//while in the lobby.
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class LobbySub60WarpToNewAreaCommandHandler : Command60Handler<Sub60WarpToNewAreaCommand>
	{
		private IEntityGuidMappable<PlayerZoneData> ZoneDataMappable { get; }

		/// <inheritdoc />
		public LobbySub60WarpToNewAreaCommandHandler(ILog logger, [NotNull] IEntityGuidMappable<PlayerZoneData> zoneDataMappable) 
			: base(logger)
		{
			ZoneDataMappable = zoneDataMappable ?? throw new ArgumentNullException(nameof(zoneDataMappable));
		}

		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60WarpToNewAreaCommand command)
		{
			//All we need to do is set the new zone for lobby.
			//We should not assume that they are ever going to leave in the lobby
			//so don't remove them even if it appears they're going to a different map/area
			//that the local player is not in.
			ZoneDataMappable[EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier)] = new PlayerZoneData(command.Zone);
			return Task.CompletedTask;
		}
	}
}
