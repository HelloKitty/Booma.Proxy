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
	public sealed partial class BlockTextChatMessageEventPayload : PSOBBGamePacketPayloadServer, IMessageContextIdentifiable
	{
		//TODO: What is this?
		[WireMember(1)]
		internal short unk { get; set; } //Syl has this as: 0x00010000

		//Tethealla sends a client id byt Sylv sends some nonsense. Not sure why.
		/// <summary>
		/// The id for the client source.
		/// </summary>
		[WireMember(2)]
		public byte Identifier { get; internal set; }

		//TODO: What is this?
		[WireMember(3)]
		internal byte unk2 { get; set; }

		/// <summary>
		/// The guild card number associated with the chat message.
		/// </summary>
		[WireMember(4)]
		public uint GuildCardNumber { get; internal set; }

		//Contains the {username}\t\tE{message}
		[Encoding(EncodingType.UTF16)]
		[WireMember(5)]
		public string ChatMessage { get; internal set; }

		public BlockTextChatMessageEventPayload()
			: base(GameNetworkOperationCode.CHAT_TYPE)
		{
			
		}
	}
}
