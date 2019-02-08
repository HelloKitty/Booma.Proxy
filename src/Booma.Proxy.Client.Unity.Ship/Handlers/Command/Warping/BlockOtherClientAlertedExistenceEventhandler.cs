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
	[AdditionalRegisterationAs(typeof(IRemoteClientAcknowledgedWarpEventSubscribable))]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class BlockOtherClientAlertedExistenceEventHandler : Command60Handler<Sub60FinishedWarpAckCommand>, IRemoteClientAcknowledgedWarpEventSubscribable //we don't need context
	{
		private IUnitScalerStrategy UnitScaler { get; }

		private IEntityGuidMappable<WorldTransform> WorldTransformMappable { get; }

		private IEntityGuidMappable<PlayerZoneData> ZoneDataMappable { get; }

		/// <inheritdoc />
		public event EventHandler<RemotePlayerWarpAcknowledgementEventArgs> OnRemotePlayerAcknowledgedWarp;

		/// <inheritdoc />
		public BlockOtherClientAlertedExistenceEventHandler([NotNull] IUnitScalerStrategy unitScaler, [NotNull] ILog logger, [NotNull] IEntityGuidMappable<WorldTransform> worldTransformMappable, [NotNull] IEntityGuidMappable<PlayerZoneData> zoneDataMappable) 
			: base(logger)
		{
			UnitScaler = unitScaler ?? throw new ArgumentNullException(nameof(unitScaler));
			WorldTransformMappable = worldTransformMappable ?? throw new ArgumentNullException(nameof(worldTransformMappable));
			ZoneDataMappable = zoneDataMappable ?? throw new ArgumentNullException(nameof(zoneDataMappable));
		}

		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60FinishedWarpAckCommand command)
		{
			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier);

			if(Logger.IsInfoEnabled)
				Logger.Info($"Client broadcasted existence Id: {command.Identifier} ZoneId: {command.ZoneId}");

			//The reason we have to do this is because remote players, that we already known about,
			//could be broadcasting out a warp ack to alert other players that they exist
			//but not intend for it to reach us really. In this case, we already have the player existing
			//so if we don't do it this way then we will end up with duplicate spawns
			if(WorldTransformMappable.ContainsKey(entityGuid) && ZoneDataMappable.ContainsKey(entityGuid))
			{
				//TODO: Should we ever assume they will ack a new zone??? Probably never legit in the lobby but might happen in games? Unsure.
				InitializeAckDataToEntityMappables(command, entityGuid);
			}
			else
			{
				HandleUnknownEntityWarpAck(command, entityGuid);
			}

			return Task.CompletedTask;
		}

		private void HandleUnknownEntityWarpAck(Sub60FinishedWarpAckCommand command, int entityGuid)
		{
			InitializeAckDataToEntityMappables(command, entityGuid);

			//At this point, it should be able to spawn the player so we should let any listeners know about
			//the ack
			OnRemotePlayerAcknowledgedWarp?.Invoke(this, new RemotePlayerWarpAcknowledgementEventArgs(entityGuid));
		}

		private void InitializeAckDataToEntityMappables(Sub60FinishedWarpAckCommand command, int entityGuid)
		{
			//We have to do basically what the 3 packet process does for newly joining clients
			//We need to create the world transform so that it will be known where to spawn.
			float rotation = UnitScaler.ScaleYRotation(command.YAxisRotation);
			Vector3 position = UnitScaler.Scale(command.Position);
			WorldTransformMappable[entityGuid] = new WorldTransform(position, Quaternion.Euler(0.0f, rotation, 0.0f));

			//Then we have to actually create/set the zone data so that
			//it's known which zone the player is in
			ZoneDataMappable[entityGuid] = new PlayerZoneData(command.ZoneId);
		}
	}
}
