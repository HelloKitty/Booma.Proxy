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
	//This command is sent by other players when we're freshly warping in so that we
	//know where they are after we spawn/warp.
	/// <summary>
	/// The handler for <see cref="Sub60ClientBurstBeginEventCommand"/> which handles this payload
	/// alerting 
	/// </summary>
	[PSOBBHandler]
	public sealed class InteropSub60FinishedWarpingBurstingCommandHandler : InteropCommand60Handler<Sub60FinishedWarpingBurstingCommand>
	{
		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		private IUnitScalerStrategy UnitScaler { get; }

		private GladMMO.IReadonlyEntityGuidMappable<GladMMO.WorldTransform> WorldTransformMappable { get; }

		private ICharacterSlotSelectedModel SlotModel { get; }

		private IReadonlyEntityGuidMappable<int, PlayerZoneData> ZoneDataMappable { get; }

		private IReadonlyEntityGuidMappable<int, GladMMO.WorldTransform> PsobbWorldTransformMappable { get; }

		private IInteropEntityMappable GuidMappable { get; }

		/// <inheritdoc />
		public InteropSub60FinishedWarpingBurstingCommandHandler(ILog logger,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] IUnitScalerStrategy unitScaler,
			[NotNull] GladMMO.IReadonlyEntityGuidMappable<GladMMO.WorldTransform> worldTransformMappable,
			[NotNull] ICharacterSlotSelectedModel slotModel,
			[NotNull] IEntityGuidMappable<int, PlayerZoneData> zoneDataMappable,
			[NotNull] IReadonlyEntityGuidMappable<int, GladMMO.WorldTransform> psobbWorldTransformMappable,
			[NotNull] IInteropEntityMappable guidMappable)
			: base(logger)
		{
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			UnitScaler = unitScaler ?? throw new ArgumentNullException(nameof(unitScaler));
			WorldTransformMappable = worldTransformMappable ?? throw new ArgumentNullException(nameof(worldTransformMappable));
			SlotModel = slotModel ?? throw new ArgumentNullException(nameof(slotModel));
			ZoneDataMappable = zoneDataMappable ?? throw new ArgumentNullException(nameof(zoneDataMappable));
			PsobbWorldTransformMappable = psobbWorldTransformMappable ?? throw new ArgumentNullException(nameof(psobbWorldTransformMappable));
			GuidMappable = guidMappable ?? throw new ArgumentNullException(nameof(guidMappable));
		}

		protected override async Task HandleSubMessage(InteropPSOBBPeerMessageContext context, Sub60FinishedWarpingBurstingCommand command)
		{
			GladMMO.WorldTransform transform = WorldTransformMappable.RetrieveEntity(PlayerDetails.LocalPlayerGuid);
			Vector3 position = new Vector3(transform.PositionX, transform.PositionY, transform.PositionZ);

			Vector3<float> scaledPosition = UnitScaler.UnScale(position).ToNetworkVector3();
			float scaledRotation = UnitScaler.ScaleYRotation(transform.YAxisRotation);

			await context.PayloadSendService.SendMessage(new Sub60FinishedWarpAckCommand(SlotModel.SlotSelected, 15, scaledPosition, scaledRotation).ToPayload());

			//TODO: Refactor, not worth the time right now.
			//GladMMO needs to spawn the player when they complete the warp.
			//This is kinda duplicate code from the WarpAck handler.
			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier);

			//This prevents disconnection by remote malicious clients.
			if(ZoneDataMappable.ContainsKey(entityGuid) && PsobbWorldTransformMappable.ContainsKey(entityGuid))
			{
				//If they are on the same floor as us.
				//TODO: Don't check 15 (lobby) check client's current floor.
				if(ZoneDataMappable[entityGuid].ZoneId == 15)
					await SpawnPlayer(context, entityGuid, PsobbWorldTransformMappable[entityGuid]);
			}
		}

		private async Task SpawnPlayer(InteropPSOBBPeerMessageContext context, int entityGuid, GladMMO.WorldTransform transform)
		{
			await context.GladMMOClientPayloadReceiver.SendMessage(new NetworkObjectVisibilityChangeEventPayload(new EntityCreationData[1] { CreateEntityData(entityGuid, transform) }, new NetworkEntityGuid[0]));
		}

		private EntityCreationData CreateEntityData(int entityGuid, GladMMO.WorldTransform transform)
		{
			return new EntityCreationData(GuidMappable[entityGuid], new PositionChangeMovementData(DateTime.UtcNow.Ticks, new Vector3(transform.PositionX, transform.PositionY, transform.PositionZ), Vector2.zero, transform.YAxisRotation), CreateInitialFieldValues());
		}

		private FieldValueUpdate CreateInitialFieldValues()
		{
			//TODO: Initialize player data somehow.
			return new FieldValueUpdate(new WireReadyBitArray(GladMMOCommonConstants.PLAYER_DATA_FIELD_SIZE * 32, false), new int[0]);
		}
	}
}
