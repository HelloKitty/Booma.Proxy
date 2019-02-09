using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	public sealed class PlayerStartPlayerWarpEventHandler : Command60Handler<Sub60StartNewWarpCommand>
	{
		private IEntityGuidMappable<PlayerZoneData> ZoneDataMappable { get; }

		/// <inheritdoc />
		public PlayerStartPlayerWarpEventHandler(ILog logger, [NotNull] IEntityGuidMappable<PlayerZoneData> zoneDataMappable) 
			: base(logger)
		{
			ZoneDataMappable = zoneDataMappable ?? throw new ArgumentNullException(nameof(zoneDataMappable));
		}

		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60StartNewWarpCommand command)
		{
			//TODO: We should remove the player's physical representation from the current map if they're in it.
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Player ID: {command.Identifier} starting warp to ZoneId: {command.ZoneId} - Unused: {command.Unused1} {command.Unused2}");

			//Even if we don't know about the entity, and have never seen it, let's assume that we will or need this data
			//and store/override the existing zone data in the ZoneMappable.

			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier);

			ZoneDataMappable[entityGuid] = new PlayerZoneData(command.ZoneId);

			return Task.CompletedTask;
		}
	}
}
