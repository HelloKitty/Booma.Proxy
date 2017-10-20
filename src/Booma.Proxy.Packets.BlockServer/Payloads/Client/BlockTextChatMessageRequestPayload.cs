using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.CHAT_TYPE)]
	public class BlockTextChatMessageRequestPayload : PSOBBGamePacketPayloadClient
	{
		//TODO: Syl doesn't deal with this, Teth sends client id. Not sure what to do. Why bother
		[WireMember(1)]
		private int Padding { get; }

		[WireMember(2)]
		private uint GuildCardNumber { get; }

		[Encoding(EncodingType.UTF16)]
		[KnownSize(2)]
		[WireMember(3)]
		private string Encoding { get; } = "\tE";

		[Encoding(EncodingType.UTF16)]
		[WireMember(4)]
		public string ChatMessage { get; }

		public BlockTextChatMessageRequestPayload([NotNull] string chatMessage)
		{
			if(string.IsNullOrWhiteSpace(chatMessage)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(chatMessage));

			ChatMessage = chatMessage;
		}

		private BlockTextChatMessageRequestPayload()
		{

		}
	}
}
