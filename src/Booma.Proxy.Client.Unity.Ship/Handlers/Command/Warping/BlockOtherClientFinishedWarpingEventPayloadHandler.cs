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
	[Injectee]
	public sealed class BlockOtherClientFinishedWarpingEventPayloadHandler : ContextExtendedCommand60Handler<Sub60FinishedWarpingBurstingCommand, INetworkPlayerFullNetworkMessageContext>
	{
		/// <summary>
		/// The scaling service.
		/// </summary>
		[Inject]
		private IUnitScalerStrategy ScalingService { get; }

		//TODO: How should this be designed?
		[SerializeField]
		public int ZoneId;

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
