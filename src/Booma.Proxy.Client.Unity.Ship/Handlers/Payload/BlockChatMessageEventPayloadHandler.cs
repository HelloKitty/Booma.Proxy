using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class BlockChatMessageEventPayloadHandler : ContextExtendedMessageHandler<BlockTextChatMessageEventPayload, INetworkPlayerNetworkMessageContext>
	{
		public BlockChatMessageEventPayloadHandler(ILog logger, [NotNull] INetworkMessageContextFactory<IMessageContextIdentifiable, INetworkPlayerNetworkMessageContext> contextFactory) 
			: base(logger, contextFactory)
		{

		}

		/// <inheritdoc />
		protected override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockTextChatMessageEventPayload payload, INetworkPlayerNetworkMessageContext payloadContext)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved Chat ClientId: {payloadContext.RemotePlayer.Identity.EntityId} GCN: {payload.GuildCardNumber} Message: {payload.ChatMessage.Aggregate("", (s, b) => $"{s} {b}")}");

			return Task.CompletedTask;
		}
	}
}
