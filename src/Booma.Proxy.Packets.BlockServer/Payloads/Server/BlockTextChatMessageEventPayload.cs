using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload sent by the server when a chat event is raised.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.CHAT_TYPE)]
	public sealed class BlockTextChatMessageEventPayload : PSOBBGamePacketPayloadServer
	{
		//TODO: What is this?
		[WireMember(1)]
		private int padding { get; } //Syl has this as: 0x00010000

		/// <summary>
		/// The guild card number associated with the chat message.
		/// </summary>
		[WireMember(2)]
		public uint GuildCardNumber { get; }

		[Encoding(EncodingType.UTF16)]
		[WireMember(3)]
		public string ChatMessage { get; }

		private BlockTextChatMessageEventPayload()
		{
			
		}
	}
}
