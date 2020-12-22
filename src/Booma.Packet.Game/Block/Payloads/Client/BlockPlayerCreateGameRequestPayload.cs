using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Game creation request sent by the player when he attempts to
	/// create a party on a block.
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.GAME_CREATE_TYPE)]
	public sealed class BlockPlayerCreateGameRequestPayload : PSOBBGamePacketPayloadClient
	{
		//Sylverant says this is unused 2 uints
		[WireMember(1)]
		private long unk1 { get; }

		/// <summary>
		/// The name of the party attempted to be created.
		/// </summary>
		[KnownSize(16)]
		[Encoding(EncodingType.UTF16)]
		[WireMember(2)]
		public string GameName { get; }

		/// <summary>
		/// The name of the party attempted to be created.
		/// </summary>
		[KnownSize(16)]
		[Encoding(EncodingType.UTF16)]
		[WireMember(3)]
		public string Password { get; }

		/// <summary>
		/// The desired difficulty of the party.
		/// </summary>
		[WireMember(4)]
		public DifficultyType PartyDifficulty { get; }

		/// <summary>
		/// Indicates if Battle mode is enabled.
		/// </summary>
		[WireMember(5)]
		public bool isBattleModeEnabled { get; }

		/// <summary>
		/// Indicates if challenge mode is enabled.
		/// </summary>
		[WireMember(6)]
		public bool isChallengeModeEnabled { get; }

		/// <summary>
		/// The episode of the party.
		/// </summary>
		[WireMember(7)]
		public EpisodeType PartyEpisode { get; }

		[WireMember(8)]
		public bool isSinglePlayerModeEnabled { get; }

		/// <inheritdoc />
		public BlockPlayerCreateGameRequestPayload(string gameName, string password, DifficultyType partyDifficulty, bool isBattleModeEnabled, bool isChallengeModeEnabled, EpisodeType partyEpisode, bool isSinglePlayerModeEnabled)
		{
			this.unk1 = unk1;
			GameName = gameName;
			Password = password;
			PartyDifficulty = partyDifficulty;
			this.isBattleModeEnabled = isBattleModeEnabled;
			this.isChallengeModeEnabled = isChallengeModeEnabled;
			PartyEpisode = partyEpisode;
			this.isSinglePlayerModeEnabled = isSinglePlayerModeEnabled;
		}

		/// <summary>
		/// Serializer ctor
		/// </summary>
		private BlockPlayerCreateGameRequestPayload()
		{
			
		}
	}
}
