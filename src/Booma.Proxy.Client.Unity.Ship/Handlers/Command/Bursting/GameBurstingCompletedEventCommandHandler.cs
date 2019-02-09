using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using UnityEngine;

namespace Booma.Proxy
{
	//TODO: Temp disabled, it didn't work. Also, it didn't make other late joining clients see us.
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	public sealed class GameBurstingCompletedEventCommandHandler : Command60Handler<Sub60GameBurstingCompleteEventCommand>
	{
		private ICharacterSlotSelectedModel SlotModel { get; }
		
		private IZoneSettings ZoneSettings { get; }

		private ILocalPlayerData PlayerData { get; }

		private IUnitScalerStrategy ScalerService { get; }

		/// <inheritdoc />
		public GameBurstingCompletedEventCommandHandler(ILog logger, [NotNull] ICharacterSlotSelectedModel slotModel, [NotNull] ILocalPlayerData playerData, [NotNull] IUnitScalerStrategy scalerService, [NotNull] IZoneSettings zoneSettings) 
			: base(logger)
		{
			SlotModel = slotModel ?? throw new ArgumentNullException(nameof(slotModel));
			PlayerData = playerData ?? throw new ArgumentNullException(nameof(playerData));
			ScalerService = scalerService ?? throw new ArgumentNullException(nameof(scalerService));
			ZoneSettings = zoneSettings ?? throw new ArgumentNullException(nameof(zoneSettings));
		}

		/// <inheritdoc />
		protected override async Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60GameBurstingCompleteEventCommand command)
		{
			//It's possible we get this on OUR join. We may not be spawned yet.
			if(!PlayerData.isWorldObjectSpawned)
				return;

			//TODO: At some point, this may not run on the main thread so this won't be safe.
			GameObject playerWorldObject = PlayerData.WorldObject;

			Vector3<float> scaledPosition = ScalerService.UnScale(playerWorldObject.transform.position).ToNetworkVector3();
			float scaledRotation = ScalerService.UnScaleYRotation(playerWorldObject.transform.rotation.y);

			//If have to send this message otherwise other client's won't know we're also in the same zone
			//It's odd, but it's something we have to do.
			await context.PayloadSendService.SendMessage(new Sub60FinishedWarpAckCommand(SlotModel.SlotSelected, ZoneSettings.ZoneId, scaledPosition, scaledRotation).ToPayload());
		}
	}
}
