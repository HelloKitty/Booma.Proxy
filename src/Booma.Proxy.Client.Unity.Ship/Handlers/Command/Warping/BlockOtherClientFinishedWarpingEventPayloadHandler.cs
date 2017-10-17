using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class BlockOtherClientFinishedWarpingEventPayloadHandler : ContextExtendedCommand60Handler<Sub60FinishedWarpingBurstingCommand, INetworkPlayerFullCommandMessageContext>
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
		protected override Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60FinishedWarpingBurstingCommand command, INetworkPlayerFullCommandMessageContext commandContext)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved finished warp from Client: {command.ClientId}");

			Vector3<float> scaledPosition = ScalingService.UnScale(commandContext.LocalPlayer.Transform.Position).ToNetworkVector3();

			//If have to send this message otherwise other client's won't know we're also in the same zone
			//It's odd, but it's something we have to do.
			context.PayloadSendService.SendMessage(new Sub60FinishedWarpAckCommand(commandContext.LocalPlayer.Identity.EntityId, ZoneId, scaledPosition).ToPayload());

			//Other clients send photon char information but I don't know what is in it yet or if it's required
			//context.PayloadSendService.SendMessage(new Sub62PhotonChairCommand().ToPayload());

			return Task.CompletedTask;
		}
	}
}
