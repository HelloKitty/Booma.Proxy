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
	public sealed class BlockBeginWarpEventPayloadHandler : GameMessageHandler<Sub60BeginWarpEventPayload>
	{
		[Inject]
		private ICharacterSlotSelectedModel SlotModel { get; }

		[SerializeField]
		private Vector3 LobbyStartPositon;

		[SerializeField]
		private byte ZoneId;

		/// <inheritdoc />
		public override async Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60BeginWarpEventPayload payload)
		{
			//TODO: What should the W coord be? How sould we handle this poition?
			//We can't do anything with the data right now
			await context.PayloadSendService.SendMessage(new Sub60TeleportToPositionPayload((short)SlotModel.SlotSelected,
				new Vector3<float>(LobbyStartPositon.x, LobbyStartPositon.x, LobbyStartPositon.z)));

			//Now we have to send a 1F to start the warp
			//Tell the server we're warping now
			await context.PayloadSendService.SendMessage(new Sub60WarpToNewAreaPayload((short)SlotModel.SlotSelected, ZoneId));

			//We can just send a finished right away, we have nothing to load really
			await context.PayloadSendService.SendMessage(new Sub60FinishedWarpingBurstingPayload());
		}
	}
}
