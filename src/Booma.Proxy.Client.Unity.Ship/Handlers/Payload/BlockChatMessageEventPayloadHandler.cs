using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace Booma.Proxy
{
	public sealed class BlockChatMessageEventPayloadHandler : ContextExtendedMessageHandler<BlockTextChatMessageEventPayload, INetworkPlayerNetworkMessageContext>
	{
		/// <inheritdoc />
		protected override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockTextChatMessageEventPayload payload, INetworkPlayerNetworkMessageContext payloadContext)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved Chat ClientId: {payloadContext.RemotePlayer.Identity.EntityId} GCN: {payload.GuildCardNumber} Message: {payload.ChatMessage.Aggregate("", (s, b) => $"{s} {b}")}");

			return Task.CompletedTask;
		}
	}
}
