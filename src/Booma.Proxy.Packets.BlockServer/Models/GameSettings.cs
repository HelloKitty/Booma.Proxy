using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	public sealed class GameSettings
	{
		/// <summary>
		/// The difficulty of the game.
		/// </summary>
		[WireMember(1)]
		public DifficultyType Difficulty { get; }

		/// <summary>
		/// Indicates if it is battlemode.
		/// </summary>
		[WireMember(2)]
		public bool isBattle { get; }

		//TODO: What is event?
		[WireMember(3)]
		public byte Event { get; }

		/// <summary>
		/// The section id associated with the game.
		/// </summary>
		[WireMember(4)]
		public SectionId Section { get; }

		/// <summary>
		/// Indicates if the game is challenge mode.
		/// </summary>
		[WireMember(5)]
		public bool isChallengeMode { get; }

		/// <summary>
		/// The random seed associated with the room.
		/// </summary>
		[WireMember(6)]
		public uint RandomSeed { get; }

		/// <summary>
		/// The episode that the game is in.
		/// </summary>
		[WireMember(7)]
		public EpisodeType Episode { get; }

		[WireMember(8)]
		private byte One { get; } = 1;

		/// <summary>
		/// Indicates if the game is single-player.
		/// </summary>
		[WireMember(9)]
		public bool isSinglePlayer { get; }

		//TODO: Are we use this is unused?
		[WireMember(10)]
		private byte unused { get; }

		//Serializer ctor
		private GameSettings()
		{
			
		}
	}
}