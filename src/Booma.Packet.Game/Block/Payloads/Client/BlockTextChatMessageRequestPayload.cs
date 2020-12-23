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
	public partial class BlockTextChatMessageRequestPayload : PSOBBGamePacketPayloadClient
	{
		//TODO: Syl doesn't deal with this, Teth sends client id. Not sure what to do. Why bother
		[WireMember(1)]
		internal int Padding { get; set; }

		[WireMember(2)]
		internal uint GuildCardNumber { get; set; }

		[Encoding(EncodingType.UTF16)]
		[KnownSize(2)]
		[WireMember(3)]
		internal string Encoding { get; set; } = "\tE";

		[Encoding(EncodingType.UTF16)]
		[WireMember(4)]
		public string ChatMessage { get; internal set; }

		public BlockTextChatMessageRequestPayload([NotNull] string chatMessage)
			: this()
		{
			if(string.IsNullOrWhiteSpace(chatMessage)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(chatMessage));

			ChatMessage = chatMessage;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public BlockTextChatMessageRequestPayload()
			: base(GameNetworkOperationCode.CHAT_TYPE)
		{

		}
	}
}
