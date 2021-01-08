using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
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
	public sealed partial class BlockOtherPlayerJoinedLobbyEventPayload : PSOBBGamePacketPayloadServer
	{
		//Sylverant sends 1 in flags (likely size of end array, maybe this supports arrays??)
		public override bool isFlagsSerialized { get; } = false;

		//Possibly Sega used the same structure as the full lobby broadcast, so this is SIZE
		//Sylverant sets this value to 1. But it's not used for anything.
		[WireMember(1)]
		internal int One1 { get; set; } = 1;

		/// <summary>
		/// The ID granted to the client that is joining the lobby.
		/// </summary>
		[WireMember(2)]
		public byte ClientId { get; internal set; }

		//TODO: What is this?
		[WireMember(3)]
		public byte LeaderId { get; internal set; }

		//Why is this in some of the packets?
		[WireMember(4)]
		internal byte One2 { get; set; }

		//Why is this sent? Shouldn't we be in the same lobby?
		/// <summary>
		/// The number of the lobby.
		/// </summary>
		[WireMember(5)]
		public byte LobbyNumber { get; internal set; }

		//Once again, why is this sent? Shouldn't we know what block we're in?
		/// <summary>
		/// The number of the block.
		/// </summary>
		[WireMember(6)]
		public short BlockNumber { get; internal set; }

		//TODO: What is this for?
		[WireMember(7)]
		public short EventId { get; internal set; }

		//Sylverant lists this as padding.
		[WireMember(8)]
		internal int Padding { get; set; }

		[WireMember(9)]
		public CharacterJoinData JoinData { get; internal set; }

		public BlockOtherPlayerJoinedLobbyEventPayload(byte clientId, byte leaderId, byte lobbyNumber, short blockNumber, short eventId, CharacterJoinData joinData) 
			: this()
		{
			ClientId = clientId;
			LeaderId = leaderId;
			LobbyNumber = lobbyNumber;
			BlockNumber = blockNumber;
			EventId = eventId;
			JoinData = joinData ?? throw new ArgumentNullException(nameof(joinData));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public BlockOtherPlayerJoinedLobbyEventPayload()
			: base(GameNetworkOperationCode.LOBBY_ADD_PLAYER_TYPE)
		{

		}	
	}
}
