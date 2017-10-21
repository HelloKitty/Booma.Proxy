using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Packet sent as a response for a game
	/// join attempt.
	/// </summary>
	[SeperatedCollectionSize(nameof(_Players), nameof(PlayerCount))]
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.GAME_JOIN_TYPE)]
	public sealed class BlockGameJoinEventPayload : PSOBBGamePacketPayloadServer, IMessageContextIdentifiable
	{
		//We want to override the flags and deserialize it as the collection size
		/// <inheritdoc />
		public override bool isFlagsSerialized => false;

		//The size to _Players
		/// <summary>
		/// The amount of players in the room.
		/// (The sixze for _Players)
		/// </summary>
		[WireMember(1)]
		internal int PlayerCount { get; }

		//TODO: What is this?
		[KnownSize(0x20)]
		[WireMember(2)]
		public uint[] Maps { get; }

		//The size is sent in Flags but we use the SeperatedCollectionSize attribute
		//feature to link them because Maps is in the way
		[SendSize(SendSizeAttribute.SizeType.Int32)]
		[WireMember(3)]
		internal PlayerInformationHeader[] _Players { get; }

		/// <summary>
		/// The identifier for the client.
		/// (ClientId)
		/// </summary>
		[WireMember(4)]
		public byte Identifier { get; }

		/// <summary>
		/// The ID of the room leader.
		/// </summary>
		[WireMember(5)]
		public byte LeaderId { get; }

		[WireMember(6)]
		private byte One { get; } = 1;

		/// <summary>
		/// The settings for the game/room.
		/// </summary>
		[WireMember(7)]
		public GameSettings Settings { get; }

		//Serializer ctor
		private BlockGameJoinEventPayload()
		{
			
		}
	}

	/*uint8_t difficulty;
uint8_t battle;
uint8_t event;
uint8_t section;
uint8_t challenge;
uint32_t rand_seed;
uint8_t episode;
uint8_t one2;
uint8_t single_player;
uint8_t unused;*/
}
