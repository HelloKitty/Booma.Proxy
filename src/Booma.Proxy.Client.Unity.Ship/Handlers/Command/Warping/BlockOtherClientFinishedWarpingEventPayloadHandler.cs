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
	[AdditionalRegisterationAs(typeof(IRemotePlayerFinishedWarpedToZoneEventSubscribable))]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	public sealed class BlockOtherClientFinishedWarpingEventPayloadHandler : Command60Handler<Sub60FinishedWarpingBurstingCommand>, IRemotePlayerFinishedWarpedToZoneEventSubscribable
	{
		/// <summary>
		/// The scaling service.
		/// </summary>
		private IUnitScalerStrategy ScalingService { get; }

		private int ZoneId { get; }

		private ILocalPlayerData LocalPlayerData { get; }

		private IReadonlyEntityGuidMappable<PlayerZoneData> PlayerZoneDataMappable { get; }

		/// <inheritdoc />
		public event EventHandler<PlayerWarpedToZoneEventArgs> OnRemotePlayedFinishedWarpToZone;

		public BlockOtherClientFinishedWarpingEventPayloadHandler([NotNull] IUnitScalerStrategy scalingService, IZoneSettings zoneSettings,
			ILog logger, [NotNull] ILocalPlayerData localPlayerData, [NotNull] IReadonlyEntityGuidMappable<PlayerZoneData> playerZoneDataMappable)
			: base(logger)
		{
			ScalingService = scalingService ?? throw new ArgumentNullException(nameof(scalingService));
			LocalPlayerData = localPlayerData ?? throw new ArgumentNullException(nameof(localPlayerData));
			PlayerZoneDataMappable = playerZoneDataMappable ?? throw new ArgumentNullException(nameof(playerZoneDataMappable));

			//We just need the zone id.
			ZoneId = zoneSettings.ZoneId;
		}

		/// <inheritdoc />
		protected override async Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60FinishedWarpingBurstingCommand command)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved finished warp from Client: {command.Identifier} SameZone: {command}");

			//TODO: Can we always assume we have a world object when we recieved this??
			if(!LocalPlayerData.isWorldObjectSpawned)
				throw new InvalidOperationException($"Recieved {nameof(Sub60FinishedWarpingBurstingCommand)} before local player exists.");

			Vector3<float> scaledPosition = ScalingService.UnScale(LocalPlayerData.WorldObject.transform.position).ToNetworkVector3();
			float scaledRotation = ScalingService.UnScaleYRotation(LocalPlayerData.WorldObject.transform.rotation.y);

			//If have to send this message otherwise other client's won't know we're also in the same zone
			//It's odd, but it's something we have to do.
			await context.PayloadSendService.SendMessage(new Sub60FinishedWarpAckCommand(LocalPlayerData.SlotIndex, ZoneId, scaledPosition, scaledRotation).ToPayload());

			//Other clients send photon char information but I don't know what is in it yet or if it's required
			await context.PayloadSendService.SendMessage(new Sub62PhotonChairCommand().ToPayload());

			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier);

			//TODO: Is it really safe to assume that they have zone data?? If they never sent it then this will throw here. Or it'll be stale.
			//TODO: Should we broadcast this event before or after the warp ack is sent?
			OnRemotePlayedFinishedWarpToZone?.Invoke(this, new PlayerWarpedToZoneEventArgs(entityGuid, PlayerZoneDataMappable[entityGuid].ZoneId));
		}
	}
}
