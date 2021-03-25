using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	//Very similar structure to LOBBY_ADD_PLAYER_TYPE.
	//See: https://github.com/Sylverant/ship_server/blob/9373df882859b234bc3e299d2e85f7b4c515d025/src/packets.h#L508
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.GAME_ADD_PLAYER_TYPE)]
	public sealed partial class BlockGamePlayerJoinedEventPayload : PSOBBGamePacketPayloadServer
	{
		/// <inheritdoc />
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
		internal byte One2 { get; set; } = 0;

		//Sylverant sets this to 0xFF for some reason.
		[WireMember(5)]
		internal byte LobbyNumber { get; set; } = 0xFF;

		//Slyverant sets both Block/EventId offsets to 1 (not block/event like in LOBBY ADD)
		[WireMember(6)]
		internal short BlockNumber { get; set; } = 1;

		[WireMember(7)]
		internal short EventId { get; set; } = 1;

		//Sylverant lists this as padding.
		[WireMember(8)]
		internal int Padding { get; set; }

		[WireMember(9)]
		public CharacterJoinData JoinData { get; internal set; }

		public BlockGamePlayerJoinedEventPayload(byte clientId, byte leaderId, CharacterJoinData joinData)
			: this()
		{
			ClientId = clientId;
			LeaderId = leaderId;
			JoinData = joinData ?? throw new ArgumentNullException(nameof(joinData));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public BlockGamePlayerJoinedEventPayload()
			: base(GameNetworkOperationCode.GAME_ADD_PLAYER_TYPE)
		{
			
		}
	}
}
