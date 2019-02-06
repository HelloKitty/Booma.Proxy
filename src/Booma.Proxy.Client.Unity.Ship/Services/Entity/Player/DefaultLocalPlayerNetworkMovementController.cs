using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	//TODO: This is a kinda a hack, to get it into the scene. We don't need init on this.
	[AdditionalRegisterationAs(typeof(ILocalPlayerNetworkMovementController))]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class DefaultLocalPlayerNetworkMovementController : ILocalPlayerNetworkMovementController, IGameInitializable
	{
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		private IUnitScalerStrategy UnitScaler { get; }

		private IZoneSettings ZoneData { get; }

		private IRoomQueryable RoomQueryService { get; }

		private ICharacterSlotSelectedModel PlayerSlotModel { get; }

		/// <inheritdoc />
		public DefaultLocalPlayerNetworkMovementController([NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService, [NotNull] IUnitScalerStrategy unitScaler, [NotNull] IZoneSettings zoneData, [NotNull] IRoomQueryable roomQueryService, [NotNull] ICharacterSlotSelectedModel playerSlotModel)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			UnitScaler = unitScaler ?? throw new ArgumentNullException(nameof(unitScaler));
			ZoneData = zoneData ?? throw new ArgumentNullException(nameof(zoneData));
			RoomQueryService = roomQueryService ?? throw new ArgumentNullException(nameof(roomQueryService));
			PlayerSlotModel = playerSlotModel ?? throw new ArgumentNullException(nameof(playerSlotModel));
		}

		/// <inheritdoc />
		public Task StopMovementAsync(Vector3 position, Quaternion rotation)
		{
			return SendService.SendMessage(new Sub60FinishedMovingCommand(PlayerSlotModel.SlotSelected,
				UnitScaler.ScaleYRotation(rotation.eulerAngles.y),
				UnitScaler.UnScale(position).ToNetworkVector3(), RoomQueryService.RoomIdForPlayerById(PlayerSlotModel.SlotSelected), ZoneData.ZoneId).ToPayload());
		}

		/// <inheritdoc />
		public Task UpdatedMovementLocation(Vector3 position, Quaternion rotation)
		{
			return SendService.SendMessage(new Sub60MovingFastPositionSetCommand(PlayerSlotModel.SlotSelected,
				UnitScaler.UnScaleYtoZ(position)).ToPayload());
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
