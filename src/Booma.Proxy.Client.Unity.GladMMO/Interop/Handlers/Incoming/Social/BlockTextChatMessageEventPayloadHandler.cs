using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladMMO;

namespace Booma.Proxy
{
	[PSOBBHandler]
	public sealed class BlockTextChatMessageEventPayloadHandler : BasePSOBBIncomingInteropPayloadHandler<BlockTextChatMessageEventPayload>
	{
		private IChatTextMessageRecievedEventPublisher ChatMessagePublisher { get; }

		private IInteropEntityMappable GuidMappable { get; }

		public BlockTextChatMessageEventPayloadHandler(ILog logger,
			[NotNull] IChatTextMessageRecievedEventPublisher chatMessagePublisher,
			[NotNull] IInteropEntityMappable guidMappable) 
			: base(logger)
		{
			ChatMessagePublisher = chatMessagePublisher ?? throw new ArgumentNullException(nameof(chatMessagePublisher));
			GuidMappable = guidMappable ?? throw new ArgumentNullException(nameof(guidMappable));
		}

		public override async Task HandleMessage(InteropPSOBBPeerMessageContext context, BlockTextChatMessageEventPayload payload)
		{
			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, payload.Identifier);
			NetworkEntityGuid guid = GuidMappable[entityGuid];

			ChatMessagePublisher.PublishEvent(this, new TextChatEventArgs(payload.ChatMessage.Replace("\t\tE", ": "), guid, ChatChannelType.Proximity));
		}
	}
}
