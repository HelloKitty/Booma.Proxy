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
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	public sealed class BlockOtherClientFinishedWarpingEventPayloadHandler : ContextExtendedCommand60Handler<Sub60FinishedWarpingBurstingCommand, INetworkPlayerFullNetworkMessageContext>
	{
		/// <summary>
		/// The scaling service.
		/// </summary>
		private IUnitScalerStrategy ScalingService { get; }

		private int ZoneId { get; }

		protected BlockOtherClientFinishedWarpingEventPayloadHandler([NotNull] IUnitScalerStrategy scalingService, IZoneSettings zoneSettings,
			ILog logger, [NotNull] INetworkMessageContextFactory<IMessageContextIdentifiable, INetworkPlayerFullNetworkMessageContext> contextFactory)
			: base(logger, contextFactory)
		{
			ScalingService = scalingService ?? throw new ArgumentNullException(nameof(scalingService));

			//We just need the zone id.
			ZoneId = zoneSettings.ZoneId;
		}

		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60FinishedWarpingBurstingCommand command, INetworkPlayerFullNetworkMessageContext commandContext)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved finished warp from Client: {command.Identifier}");

			Vector3<float> scaledPosition = ScalingService.UnScale(commandContext.LocalPlayer.Transform.Position).ToNetworkVector3();
			float scaledRotation = ScalingService.UnScaleYRotation(commandContext.LocalPlayer.Transform.Rotation.y);

			//If have to send this message otherwise other client's won't know we're also in the same zone
			//It's odd, but it's something we have to do.
			context.PayloadSendService.SendMessage(new Sub60FinishedWarpAckCommand(commandContext.LocalPlayer.Identity.EntityId, ZoneId, scaledPosition, scaledRotation).ToPayload());

			//Other clients send photon char information but I don't know what is in it yet or if it's required
			context.PayloadSendService.SendMessage(new Sub62PhotonChairCommand().ToPayload());

			return Task.CompletedTask;
		}
	}
}
