using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/*
	 * uint32_t menu_id;
		uint32_t item_id;
		uint8_t difficulty;
		uint8_t players;
		uint16_t name[16];
		uint8_t episode;
		uint8_t flags;
	 */

	/// <summary>
	/// Data model for a game list entry.
	/// </summary>
	[WireDataContract]
	public sealed class GameListEntry
	{
		/// <summary>
		/// Listing information.
		/// </summary>
		[WireMember(1)]
		public MenuItemIdentifier Listing { get; internal set; }

		/// <summary>
		/// The difficulty mode of the game.
		/// </summary>
		[WireMember(2)]
		public DifficultyType Difficulty { get; internal set; }

		/// <summary>
		/// The amount of player's in the game.
		/// </summary>
		[WireMember(3)]
		public byte PlayerCount { get; internal set; }

		/// <summary>
		/// The name of the game.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[DontTerminate]
		[KnownSize(16)]
		[WireMember(4)]
		public string Name { get; internal set; }

		/// <summary>
		/// The episode of the game
		/// </summary>
		[WireMember(5)]
		public EpisodeType Episode { get; internal set; }

		//TODO: What is this?
		[WireMember(6)]
		internal byte flags { get; set; }

		public GameListEntry([NotNull] MenuItemIdentifier listing, 
			DifficultyType difficulty, 
			byte playerCount,
			[NotNull] string name, 
			EpisodeType episode, 
			byte flags)
		: this()
		{
			if (!Enum.IsDefined(typeof(DifficultyType), difficulty)) throw new InvalidEnumArgumentException(nameof(difficulty), (int) difficulty, typeof(DifficultyType));
			if (!Enum.IsDefined(typeof(EpisodeType), episode)) throw new InvalidEnumArgumentException(nameof(episode), (int) episode, typeof(EpisodeType));
			Listing = listing ?? throw new ArgumentNullException(nameof(listing));
			Difficulty = difficulty;
			PlayerCount = playerCount;
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Episode = episode;
			this.flags = flags;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public GameListEntry()
		{
			
		}
	}
}
