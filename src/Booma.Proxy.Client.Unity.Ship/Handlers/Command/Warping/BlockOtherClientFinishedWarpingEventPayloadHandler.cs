using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class BlockOtherClientFinishedWarpingEventPayloadHandler : Command60Handler<Sub60FinishedWarpingBurstingCommand>
	{
		[Inject]
		private ICharacterSlotSelectedModel SlotModel { get; }

		[SerializeField]
		public Vector3 TestPosition;

		[SerializeField]
		public int ZoneId;

		/// <inheritdoc />
		protected override Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60FinishedWarpingBurstingCommand command)
		{
			//If have to send this message otherwise other client's won't know we're also in the same zone
			//It's odd, but it's something we have to do.
			context.PayloadSendService.SendMessage(new Sub60FinishedWarpAckCommand((byte)SlotModel.SlotSelected, ZoneId, new Vector3<float>(TestPosition.x, TestPosition.y, TestPosition.z)).ToPayload());

			//Other clients send photon char information but I don't know what is in it yet or if it's required
			//context.PayloadSendService.SendMessage(new Sub62PhotonChairCommand().ToPayload());

			return Task.CompletedTask;
		}
	}
}
