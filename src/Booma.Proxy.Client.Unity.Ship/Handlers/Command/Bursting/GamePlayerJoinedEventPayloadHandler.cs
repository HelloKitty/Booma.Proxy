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

		private IBurstingService BurstingService { get; }

		/// <inheritdoc />
		public GamePlayerJoinedEventPayloadHandler(ILog logger, [NotNull] ICharacterSlotSelectedModel slotModel, [NotNull] IBurstingService burstingService) 
			: base(logger)
		{
			SlotModel = slotModel ?? throw new ArgumentNullException(nameof(slotModel));
			BurstingService = burstingService ?? throw new ArgumentNullException(nameof(burstingService));
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockGamePlayerJoinedEventPayload payload)
		{
			//When this join is recieved, then we have to set the bursting state so it can be remembered, referenced or cleaned up.
			if(BurstingService.SetBurstingEntity(EntityGuid.ComputeEntityGuid(EntityType.Player, payload.Identifier)))
			{
				//TODO: We are creating a fake 0x6D 0x70 here. Do we ever need a real one??
				await context.PayloadSendService.SendMessage(new BlockNetworkCommand6DEventClientPayload(payload.Identifier, new Sub6DFakePlayerJoinDataNeededCommand(SlotModel.SlotSelected)));
			}

			//TODO: What do we do if this fails?
		}
	}
}
