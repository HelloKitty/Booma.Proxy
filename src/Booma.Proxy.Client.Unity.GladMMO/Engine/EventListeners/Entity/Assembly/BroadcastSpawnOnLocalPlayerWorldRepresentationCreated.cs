using System;
using System.Collections.Generic;
using System.Text;
using GladMMO;
using GladNet;
using UnityEngine;

namespace Booma.Proxy
{
	[SceneTypeCreateGladMMO(GladMMO.GameSceneType.InstanceServerScene)]
	public sealed class BroadcastSpawnOnLocalPlayerWorldRepresentationCreatedEventListener : OnLocalPlayerSpawnedEventListener
	{
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		private IUnitScalerStrategy UnitScaler { get; }

		private GladMMO.IReadonlyEntityGuidMappable<GameObject> WorldObjectMappable { get; }

		private ICharacterSlotSelectedModel SlotModel { get; }

		public BroadcastSpawnOnLocalPlayerWorldRepresentationCreatedEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService,
			[NotNull] IUnitScalerStrategy unitScaler,
			[NotNull] GladMMO.IReadonlyEntityGuidMappable<GameObject> worldObjectMappable,
			[NotNull] ICharacterSlotSelectedModel slotModel) : base(subscriptionService)
		{
			SendService = sendService;
			UnitScaler = unitScaler ?? throw new ArgumentNullException(nameof(unitScaler));
			WorldObjectMappable = worldObjectMappable ?? throw new ArgumentNullException(nameof(worldObjectMappable));
			SlotModel = slotModel ?? throw new ArgumentNullException(nameof(slotModel));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			GameObject worldObject = WorldObjectMappable.RetrieveEntity(args.EntityGuid);

			SendService.SendMessage(new Sub60TeleportToPositionCommand(SlotModel.SlotSelected,
				UnitScaler.UnScale(worldObject.transform.position).ToNetworkVector3()).ToPayload());

			//TODO: Do map/zone settings better.
			//Now we have to send a 1F to start the warp
			//Tell the server we're warping now
			SendService.SendMessage(new Sub60WarpToNewAreaCommand(SlotModel.SlotSelected, 15).ToPayload());

			//TODO: is it save to send this in the lobby??
			SendService.SendMessage(new Sub60FinishedMapLoadCommand(SlotModel.SlotSelected).ToPayload());

			//TODO: Should we send ClientId with this one too?
			//We can just send a finished right away, we have nothing to load really
			SendService.SendMessage(new Sub60FinishedWarpingBurstingCommand(SlotModel.SlotSelected).ToPayload());
		}
	}
}
