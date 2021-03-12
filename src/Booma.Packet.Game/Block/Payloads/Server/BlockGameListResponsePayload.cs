using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booma.UI;
using FreecraftCore.Serializer;

namespace Booma
{
	//Similar to all menu list packets.
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.GAME_LIST_TYPE)]
	public sealed partial class BlockGameListResponsePayload : PSOBBGamePacketPayloadServer
	{
		//Disable flags serialization so that the ship can get the 4 byte length and
		//handle writing the 4 bytes length
		/// <inheritdoc />
		public override bool isFlagsSerialized { get; } = false;

		/// <summary>
		/// Ship list sends with a hidden entry at the top for some reason.
		/// This is a default entry that can be used.
		/// </summary>
		private static GameListEntry DefaultHiddenEntry { get; } = new GameListEntry(new MenuItemIdentifier((uint)KnownMenuIdentifier.GAME_TYPE, uint.MaxValue), DifficultyType.Normal, 0, String.Empty, EpisodeType.EpisodeI, 0);

		//PSOBB sends 4 byte Flags with the entry count. We disable Flags though to steal the 4 bytes
		[SendSize(PrimitiveSizeType.Int32)] //for some reason they send 1 less than the actual size 
		[WireMember(1)]
		internal GameListEntry[] _MenuListings { get; set; }

		//First entry is garbage entry and they length-prefix wrong length of entries for some reason.
		[WireMember(2)]
		internal GameListEntry LastGameEntry { get; set; }

		/// <summary>
		/// All the menu listings sent in the packet.
		/// </summary>
		public IEnumerable<GameListEntry> MenuListings => EnumerateListings();

		private IEnumerable<GameListEntry> EnumerateListings()
		{
			foreach(var entry in _MenuListings)
				yield return entry;

			yield return LastGameEntry;
		}

		/// <summary>
		/// Only the game menu listings sent in the packet.
		/// </summary>
		public IEnumerable<GameListEntry> Games => EnumerateGames();

		private IEnumerable<GameListEntry> EnumerateGames()
		{
			//First hidden entry is skipped.
			return EnumerateListings()
				.Skip(1);
		}

		/// <summary>
		/// Creates a new game list packet with the provided games.
		/// </summary>
		/// <param name="gameList">The list of ships.</param>
		public BlockGameListResponsePayload([NotNull] GameListEntry[] gameList)
			: this()
		{
			if(gameList == null) throw new ArgumentNullException(nameof(gameList));

			//Special cast for game list entry of 1
			if(gameList.Length == 0)
			{
				_MenuListings = Array.Empty<GameListEntry>();
				LastGameEntry = DefaultHiddenEntry;
			}
			else
			{
				_MenuListings = new[] {DefaultHiddenEntry}
					.Concat(gameList)
					.Take(gameList.Length)
					.ToArray();

				LastGameEntry = gameList.Last();
			}
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public BlockGameListResponsePayload()
			: base(GameNetworkOperationCode.GAME_LIST_TYPE)
		{

		}
	}
}
