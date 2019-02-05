using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class AreaTeleportOnLocalPlayerSpawnedEventListener : BaseSingleEventListenerInitializable<ILocalPlayerWorldRepresentationSpawnedEventSubscribable, LocalPlayerWorldObjectSpawnedEventArgs>
	{
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		private IUnitScalerStrategy ScalerService { get; }

		private IZoneSettings ZoneSettings { get; }

		/// <inheritdoc />
		public AreaTeleportOnLocalPlayerSpawnedEventListener(ILocalPlayerWorldRepresentationSpawnedEventSubscribable subscriptionService, [NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService, [NotNull] IUnitScalerStrategy scalerService, [NotNull] IZoneSettings zoneSettings) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			ScalerService = scalerService ?? throw new ArgumentNullException(nameof(scalerService));
			ZoneSettings = zoneSettings ?? throw new ArgumentNullException(nameof(zoneSettings));
		}

		/// <inheritdoc />
		protected override async void OnEventFired(object source, LocalPlayerWorldObjectSpawnedEventArgs args)
		{
			//TODO: We should extract this into a warping service.

			//TODO: Send rotation
			//TODO: What should the W coord be? How sould we handle this poition?
			//We can't do anything with the data right now
			await SendService.SendMessage(new Sub60TeleportToPositionCommand((byte)EntityGuid.GetEntityId(args.EntityGuid),
				ScalerService.UnScale(args.WorldObject.transform.position).ToNetworkVector3()).ToPayload());

			//Now we have to send a 1F to start the warp
			//Tell the server we're warping now
			await SendService.SendMessage(new Sub60WarpToNewAreaCommand((byte)EntityGuid.GetEntityId(args.EntityGuid), ZoneSettings.ZoneId).ToPayload());

			//TODO: Should we send ClientId with this one too?
			//We can just send a finished right away, we have nothing to load really
			await SendService.SendMessage(new Sub60FinishedWarpingBurstingCommand().ToPayload());
		}
	}
}
