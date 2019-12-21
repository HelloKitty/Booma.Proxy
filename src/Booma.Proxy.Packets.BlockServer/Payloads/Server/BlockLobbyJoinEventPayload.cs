using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Packet sent to the client telling it to join a lobby.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.LOBBY_JOIN_TYPE)]
	public sealed class BlockLobbyJoinEventPayload : PSOBBGamePacketPayloadServer
	{
		//TODO: We can't currently handle this packet. It does something odd the serializer can't handle
		/// <summary>
		/// The ID granted to the client that is joining the lobby.
		/// </summary>
		[WireMember(1)]
		public byte ClientId { get; }

		//TODO: What is this?
		[WireMember(2)]
		public byte LeaderId { get; }

		//Why is this in some of the packets?
		[WireMember(3)]
		private byte One { get; }

		//Why is this sent? Shouldn't we be in the same lobby?
		/// <summary>
		/// The number of the lobby being joined.
		/// </summary>
		[WireMember(4)]
		public byte LobbyNumber { get; }

		//Once again, why is this sent? Shouldn't we know what block we're in?
		/// <summary>
		/// The number of the block.
		/// </summary>
		[WireMember(5)]
		public short BlockNumber { get; }

		//TODO: What is this for?
		[WireMember(6)]
		public short EventId { get; }

		//Sylverant lists this as padding.
		[WireMember(7)]
		private int Padding { get; }

		//TODO: There is more to the packet here: https://github.com/Sylverant/ship_server/blob/b3bffc84b558821ca2002775ab2c3af5c6dde528/src/packets.h#L517

		[WireMember(8)]
		public CharacterJoinData LocalPlayerData { get; }

		[WireMember(9)]
		private short unk2 { get; }

		//There could be 15 here but it actually depends on the Lobby count
		//and it isn't length-prefixed sadly.
		//[SendSize(SendSizeAttribute.SizeType.UShort)]
		[ReadToEnd]
		[WireMember(10)]
		public CharacterJoinData[] LobbyCharacterData { get; }

		private BlockLobbyJoinEventPayload()
		{
			
		}
	}
}
