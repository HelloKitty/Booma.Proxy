using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	public sealed class GameSetCharacterDataEventPayloadHandler : GameMessageHandler<BlockSetCharacterDataEventPayload>
	{
		private ICharacterSlotSelectedModel SlotModel { get; }

		/// <inheritdoc />
		public GameSetCharacterDataEventPayloadHandler(ILog logger, ICharacterSlotSelectedModel slotModel)
			: base(logger)
		{
			SlotModel = slotModel;
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockSetCharacterDataEventPayload payload)
		{
			//TODO: We used to send this here, but it doesn't need to be that way. We need to properly handle this packet.
			//await context.PayloadSendService.SendMessage(new BlockNetworkCommand6DEventClientPayload(GamePlayerJoinedEventPayloadHandler.PlayerBurstingId, new Sub6DFakePlayerJoinDataNeededCommand(SlotModel.SlotSelected)));
		}
	}
}
