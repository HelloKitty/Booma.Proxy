using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Packet sent as a response for a game
	/// join attempt.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.GAME_JOIN_TYPE)]
	public sealed partial class BlockGameJoinEventPayload : PSOBBGamePacketPayloadServer, IMessageContextIdentifiable
	{
		//We want to override the flags and deserialize it as the count of players in the room
		/// <inheritdoc />
		public override bool isFlagsSerialized => false;

		/// <summary>
		/// The amount of players in the room.
		/// </summary>
		[WireMember(1)]
		public int PlayerCount { get; internal set; }

		//TODO: What is this?
		[KnownSize(0x20)]
		[WireMember(2)]
		public uint[] Maps { get; internal set; }

		//It will always send 4 slots but some may be uninitialized
		[KnownSize(4)]
		[WireMember(3)]
		internal PlayerInformationHeader[] _Players { get; set; }

		/// <summary>
		/// The players currently in the game/room.
		/// </summary>
		public IEnumerable<PlayerInformationHeader> Players => _Players.Where(p => p.isSlotFilled);

		/// <summary>
		/// The identifier for the client.
		/// (ClientId)
		/// </summary>
		[WireMember(4)]
		public byte Identifier { get; internal set; }

		/// <summary>
		/// The ID of the room leader.
		/// </summary>
		[WireMember(5)]
		public byte LeaderId { get; internal set; }

		[WireMember(6)]
		internal byte One { get; set; } = 1;

		/// <summary>
		/// The settings for the game/room.
		/// </summary>
		[WireMember(7)]
		public GameSettings Settings { get; internal set; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public BlockGameJoinEventPayload()
			: base(GameNetworkOperationCode.GAME_JOIN_TYPE)
		{
			
		}
	}
}
