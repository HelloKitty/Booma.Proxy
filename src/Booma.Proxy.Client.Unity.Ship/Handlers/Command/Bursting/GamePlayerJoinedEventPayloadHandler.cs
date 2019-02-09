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
	public sealed class GamePlayerJoinedEventPayloadHandler : GameMessageHandler<BlockGamePlayerJoinedEventPayload>
	{

		private ICharacterSlotSelectedModel SlotModel { get; }

		/// <inheritdoc />
		public GamePlayerJoinedEventPayloadHandler(ILog logger, [NotNull] ICharacterSlotSelectedModel slotModel) 
			: base(logger)
		{
			SlotModel = slotModel ?? throw new ArgumentNullException(nameof(slotModel));
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockGamePlayerJoinedEventPayload payload)
		{
			//TODO: We are creating a fake 0x6D 0x70 here. Do we ever need a real one??

			//TODO: We are currently testing sending this in 15EA.
			//TODO: We are creating a fake 0x6D 0x70 here. Do we ever need a real one??
			await context.PayloadSendService.SendMessage(new BlockNetworkCommand6DEventClientPayload(payload.Identifier, new Sub6DFakePlayerJoinDataNeededCommand(SlotModel.SlotSelected)));
		}
	}
}
