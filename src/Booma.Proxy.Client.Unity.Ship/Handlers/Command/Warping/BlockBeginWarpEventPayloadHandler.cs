using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class BlockBeginWarpEventPayloadHandler : Command60Handler<Sub60ClientWarpBeginEventCommand>
	{
		/// <summary>
		/// The slot/clientid model.
		/// </summary>
		[Inject]
		private ICharacterSlotSelectedModel SlotModel { get; }

		//This is reused from an old project. It should work ok though.
		[OdinSerialize]
		private ISpawnPointStrategy SpawnPoint { get; set; }
		
		//TODO: How should we handle zone id?
		[SerializeField]
		private byte ZoneId;

		/// <inheritdoc />
		protected override async Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60ClientWarpBeginEventCommand payload)
		{
			//TODO: Send rotation
			//TODO: What should the W coord be? How sould we handle this poition?
			//We can't do anything with the data right now
			await context.PayloadSendService.SendMessage(new Sub60TeleportToPositionCommand(SlotModel.SlotSelected, 
				SpawnPoint.GetSpawnpoint().position.ToNetworkVector3()).ToPayload());

			//Now we have to send a 1F to start the warp
			//Tell the server we're warping now
			await context.PayloadSendService.SendMessage(new Sub60WarpToNewAreaCommand(SlotModel.SlotSelected, ZoneId).ToPayload());

			//TODO: Should we send ClientId with this one too?
			//We can just send a finished right away, we have nothing to load really
			await context.PayloadSendService.SendMessage(new Sub60FinishedWarpingBurstingCommand().ToPayload());
		}
	}
}
