using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/*typedef struct bb_lobby_join {
		bb_pkt_hdr_t hdr;
		uint8_t client_id;
		uint8_t leader_id;
		uint8_t one;                        Always 1
		uint8_t lobby_num;
		uint16_t block_num;
		uint16_t event;
		uint32_t padding;

		struct {

		bb_player_hdr_t hdr;
		sylverant_inventory_t inv;
		sylverant_bb_char_t data;
		}
		entries[0];
		} PACKED bb_lobby_join_pkt;*/

	//Syl handling: https://github.com/Sylverant/ship_server/blob/b3bffc84b558821ca2002775ab2c3af5c6dde528/src/ship_packets.c#L1907
	//Syl struct: https://github.com/Sylverant/ship_server/blob/b3bffc84b558821ca2002775ab2c3af5c6dde528/src/packets.h#L517
	/// <summary>
	/// Payload broadcast to all other player's in the lobby
	/// when a new character enters the lobby.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.LOBBY_ADD_PLAYER_TYPE)]
	public sealed class BlockOtherPlayerJoinedLobbyEventPayload : PSOBBGamePacketPayloadServer
	{
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
		/// The number of the lobby.
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

		//TODO: Implement character stuff later
		//TODO: Combine this into a conslidated object
		[WireMember(7)]
		public PlayerInformationHeader PlayerInfoHeader { get; }

		//Serializer ctor
		private BlockOtherPlayerJoinedLobbyEventPayload()
		{
			
		}	
	}
}
