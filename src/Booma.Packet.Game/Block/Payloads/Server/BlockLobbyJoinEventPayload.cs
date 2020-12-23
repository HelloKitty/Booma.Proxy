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
	public sealed partial class BlockLobbyJoinEventPayload : PSOBBGamePacketPayloadServer
	{
		//TODO: We can't currently handle this packet. It does something odd the serializer can't handle
		/// <summary>
		/// The ID granted to the client that is joining the lobby.
		/// 0x08
		/// </summary>
		[WireMember(1)]
		public byte ClientId { get; internal set; }

		//TODO: What is this?
		/// <summary>
		/// 0x09
		/// </summary>
		[WireMember(2)]
		public byte LeaderId { get; internal set; }

		//Why is this in some of the packets?
		/// <summary>
		/// 0x0A
		/// </summary>
		[WireMember(3)]
		internal byte One { get; set; }

		//Why is this sent? Shouldn't we be in the same lobby?
		/// <summary>
		/// The number of the lobby being joined.
		/// 0x0B
		/// </summary>
		[WireMember(4)]
		public byte LobbyNumber { get; internal set; }

		//Once again, why is this sent? Shouldn't we know what block we're in?
		/// <summary>
		/// The number of the block.
		/// 0x0C
		/// </summary>
		[WireMember(5)]
		public short BlockNumber { get; internal set; }

		//TODO: What is this for?
		/// <summary>
		/// 0x0E
		/// </summary>
		[WireMember(6)]
		public short EventId { get; internal set; }

		//Sylverant lists this as padding.
		[WireMember(7)]
		internal int Padding { get; set; }

		//TODO: There is more to the packet here: https://github.com/Sylverant/ship_server/blob/b3bffc84b558821ca2002775ab2c3af5c6dde528/src/packets.h#L517
		[ReadToEnd]
		[WireMember(10)]
		public CharacterJoinData[] LobbyCharacterData { get; internal set; }

		public BlockLobbyJoinEventPayload()
			: base(GameNetworkOperationCode.LOBBY_JOIN_TYPE)
		{
			
		}
	}
}
