using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class BlockLoginJoinEventPayloadHandler : GameMessageHandler<BlockLobbyJoinEventPayload>
	{
		//TODO: Is it ok to reuse this?
		[Inject]
		private ICharacterSlotSelectedModel SlotModel { get; }

		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, BlockLobbyJoinEventPayload payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"**Handling**: {nameof(BlockLobbyJoinEventPayload)}");

			//Just set the old char slot to the clientid
			//It's basically like a slot, like a lobby or party slot.
			SlotModel.SlotSelected = payload.ClientId;

			//Don't send anything here, the server will send a 0x60 0x6F after this
			return Task.CompletedTask;
		}
	}
}
