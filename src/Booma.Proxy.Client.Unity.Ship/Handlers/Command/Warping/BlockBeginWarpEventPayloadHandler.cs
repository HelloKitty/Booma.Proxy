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
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class BlockBeginWarpEventPayloadHandler : Command60Handler<Sub60ClientWarpBeginEventCommand>
	{
		private INetworkPlayerFactory PlayerFactory { get; }

		private IUnitScalerStrategy ScalerService { get; }
		
		private byte ZoneId { get; }

		/// <inheritdoc />
		public BlockBeginWarpEventPayloadHandler(ILog logger, [NotNull] INetworkPlayerFactory playerFactory, [NotNull] IUnitScalerStrategy scalerService, [NotNull] IZoneSettings zoneSettingsData) 
			: base(logger)
		{
			if(playerFactory == null) throw new ArgumentNullException(nameof(playerFactory));
			if(scalerService == null) throw new ArgumentNullException(nameof(scalerService));
			if(zoneSettingsData == null) throw new ArgumentNullException(nameof(zoneSettingsData));

			PlayerFactory = playerFactory;
			ScalerService = scalerService;

			//So instead of serializing the zone id we inject zone settings.
			ZoneId = (byte)zoneSettingsData.ZoneId;
		}

		/// <inheritdoc />
		protected override async Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60ClientWarpBeginEventCommand payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved: {this.MessageName()} about to create local player.");

			INetworkPlayer player = null;
			try
			{
				//TODO: Is this where we should do this?
				//We need to create the player represenation here
				player = PlayerFactory.CreateLocalPlayer();
			}
			catch(Exception e)
			{
				if(Logger.IsErrorEnabled || Logger.IsFatalEnabled)
					Logger.Fatal($"Failed to create network player. Exception: {e.Message} \n\n Stacktrace: {e.StackTrace}");
				throw;
			}
			

			//TODO: Send rotation
			//TODO: What should the W coord be? How sould we handle this poition?
			//We can't do anything with the data right now
			await context.PayloadSendService.SendMessage(new Sub60TeleportToPositionCommand((byte)player.Identity.EntityId,
				ScalerService.UnScale(player.Transform.Position).ToNetworkVector3()).ToPayload());

			//Now we have to send a 1F to start the warp
			//Tell the server we're warping now
			await context.PayloadSendService.SendMessage(new Sub60WarpToNewAreaCommand((byte)player.Identity.EntityId, ZoneId).ToPayload());

			//TODO: Should we send ClientId with this one too?
			//We can just send a finished right away, we have nothing to load really
			await context.PayloadSendService.SendMessage(new Sub60FinishedWarpingBurstingCommand().ToPayload());
		}
	}
}
