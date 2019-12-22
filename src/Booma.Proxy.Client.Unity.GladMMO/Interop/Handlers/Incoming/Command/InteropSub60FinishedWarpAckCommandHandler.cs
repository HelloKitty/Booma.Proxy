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
	//Handles packet that comes in when other clients Ack our warping.
	//The ack contains floor/zone they are in so we can use this to determine if we should actually
	//spawn them in.
	[PSOBBHandler]
	public sealed class InteropSub60FinishedWarpAckCommandHandler : InteropCommand60Handler<Sub60FinishedWarpAckCommand>
	{
		private IEntityGuidMappable<int, PlayerZoneData> ZoneDataMappable { get; }

		private IInteropEntityMappable PsoEntityKeyToGuidMappable { get; }

		private IUnitScalerStrategy UnitScaler { get; }

		public InteropSub60FinishedWarpAckCommandHandler(ILog logger, [NotNull] IEntityGuidMappable<int, PlayerZoneData> zoneDataMappable, [NotNull] IInteropEntityMappable psoEntityKeyToGuidMappable, [NotNull] IUnitScalerStrategy unitScaler)
			: base(logger)
		{
			ZoneDataMappable = zoneDataMappable ?? throw new ArgumentNullException(nameof(zoneDataMappable));
			PsoEntityKeyToGuidMappable = psoEntityKeyToGuidMappable ?? throw new ArgumentNullException(nameof(psoEntityKeyToGuidMappable));
			UnitScaler = unitScaler ?? throw new ArgumentNullException(nameof(unitScaler));
		}

		protected override async Task HandleSubMessage(InteropPSOBBPeerMessageContext context, Sub60FinishedWarpAckCommand command)
		{
			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier);

			if (ZoneDataMappable.ContainsKey(entityGuid))
			{
				//If they have changed floors we may need to spawn them
				//TODO: Don't check 15 (lobby) check client's current floor.
				if(ZoneDataMappable[entityGuid].ZoneId != command.ZoneId && command.ZoneId == 15)
					await SpawnPlayer(context, command);
			}
			else
				await SpawnPlayer(context, command);

			ZoneDataMappable[entityGuid] = new PlayerZoneData(command.ZoneId);
		}

		private async Task SpawnPlayer(InteropPSOBBPeerMessageContext context, Sub60FinishedWarpAckCommand command)
		{
			if (Logger.IsDebugEnabled)
				Logger.Debug($"Slot: {command.Identifier} warp ack'd in same floor. Requesting entity spawn.");

			await context.GladMMOClientPayloadReceiver.SendMessage(new NetworkObjectVisibilityChangeEventPayload(new EntityCreationData[1] {CreateEntityData(command)}, new NetworkEntityGuid[0]));
		}

		private EntityCreationData CreateEntityData(Sub60FinishedWarpAckCommand command)
		{
			float rotation = UnitScaler.ScaleYRotation(command.YAxisRotation);
			Vector3 position = UnitScaler.Scale(command.Position);

			return new EntityCreationData(PsoEntityKeyToGuidMappable[EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier)], new PositionChangeMovementData(DateTime.UtcNow.Ticks, position, Vector2.zero, rotation), CreateInitialFieldValues());
		}

		private FieldValueUpdate CreateInitialFieldValues()
		{
			//TODO: Initialize player data somehow.
			return new FieldValueUpdate(new WireReadyBitArray(GladMMOCommonConstants.PLAYER_DATA_FIELD_SIZE * 32, false), new int[0]);
		}
	}
}
