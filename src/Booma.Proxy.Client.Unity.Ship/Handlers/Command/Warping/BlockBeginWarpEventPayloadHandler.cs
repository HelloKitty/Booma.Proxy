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
	public sealed class BlockBeginWarpEventPayloadHandler : Command60Handler<Sub60ClientWarpBeginEventCommand>
	{
		[Inject]
		private ICharacterSlotSelectedModel SlotModel { get; }

		[SerializeField]
		private Vector3 LobbyStartPositon;

		[SerializeField]
		private byte ZoneId;

		private int Count = 0;

		/// <inheritdoc />
		protected override async Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60ClientWarpBeginEventCommand payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"**Recieved**: {nameof(Sub60ClientWarpBeginEventCommand)} Count: {Count}");

			Count++;

			//TODO: What should the W coord be? How sould we handle this poition?
			//We can't do anything with the data right now
			await context.PayloadSendService.SendMessage(new Sub60TeleportToPositionCommand(SlotModel.SlotSelected,
				new Vector3<float>(LobbyStartPositon.x, LobbyStartPositon.x, LobbyStartPositon.z)).ToPayload());

			//Now we have to send a 1F to start the warp
			//Tell the server we're warping now
			await context.PayloadSendService.SendMessage(new Sub60WarpToNewAreaCommand(SlotModel.SlotSelected, ZoneId).ToPayload());

			//We can just send a finished right away, we have nothing to load really
			await context.PayloadSendService.SendMessage(new Sub60FinishedWarpingBurstingCommand().ToPayload());
		}
	}
}
