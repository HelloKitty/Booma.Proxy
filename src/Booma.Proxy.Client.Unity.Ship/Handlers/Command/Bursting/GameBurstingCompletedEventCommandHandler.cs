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
	[AdditionalRegisterationAs(typeof(IClientFinishedBurstingEventSubscribable))]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.RagolDefault)] //bursting happens pretty much anywhere
	public sealed class GameBurstingCompletedEventCommandHandler : Command60Handler<Sub60GameBurstingCompleteEventCommand>, IClientFinishedBurstingEventSubscribable
	{
		//TODO: Refactor
		private ICharacterSlotSelectedModel SlotModel { get; }
		
		private IZoneSettings ZoneSettings { get; }

		private ILocalPlayerData PlayerData { get; }

		private IUnitScalerStrategy ScalerService { get; }

		private IBurstingService BurstingService { get; }

		/// <inheritdoc />
		public event EventHandler<ClientBurstingEndingEventArgs> OnClientBurstingFinished;

		/// <inheritdoc />
		public GameBurstingCompletedEventCommandHandler(ILog logger, [NotNull] ICharacterSlotSelectedModel slotModel, [NotNull] ILocalPlayerData playerData, [NotNull] IUnitScalerStrategy scalerService, [NotNull] IZoneSettings zoneSettings, [NotNull] IBurstingService burstingService) 
			: base(logger)
		{
			SlotModel = slotModel ?? throw new ArgumentNullException(nameof(slotModel));
			PlayerData = playerData ?? throw new ArgumentNullException(nameof(playerData));
			ScalerService = scalerService ?? throw new ArgumentNullException(nameof(scalerService));
			ZoneSettings = zoneSettings ?? throw new ArgumentNullException(nameof(zoneSettings));
			BurstingService = burstingService ?? throw new ArgumentNullException(nameof(burstingService));
		}

		/// <inheritdoc />
		protected override async Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60GameBurstingCompleteEventCommand command)
		{
			//It's possible we get this on OUR join. We may not be spawned yet.
			if(!PlayerData.isWorldObjectSpawned)
				return;

			//This could be for a couple of reasons. Bursting wasn't set, and it's a BIG failure
			//or this is our JOIN bursting finish and we don't do anything here really.
			if(!BurstingService.isBurstingInProgress)
				return;

			//TODO: At some point, this may not run on the main thread so this won't be safe.
			GameObject playerWorldObject = PlayerData.WorldObject;

			Vector3<float> scaledPosition = ScalerService.UnScale(playerWorldObject.transform.position).ToNetworkVector3();
			float scaledRotation = ScalerService.UnScaleYRotation(playerWorldObject.transform.rotation.y);

			//If have to send this message otherwise other client's won't know we're also in the same zone
			//It's odd, but it's something we have to do.
			await context.PayloadSendService.SendMessage(new Sub60FinishedWarpAckCommand(SlotModel.SlotSelected, ZoneSettings.ZoneId, scaledPosition, scaledRotation).ToPayload());

			int entityGuid = BurstingService.BurstingEntityGuid.Value;

			//Successful burst, let everyone know.
			OnClientBurstingFinished?.Invoke(this, new ClientBurstingEndingEventArgs(entityGuid, true));

			//Bursting is done, we should release bursting state
			//Then we should broadcast to everyone that bursting is done.
			BurstingService.ClearBursting();
		}
	}
}
