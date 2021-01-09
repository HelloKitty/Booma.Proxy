using FreecraftCore.Serializer;

namespace Booma
{
	[WireDataContract]
	public sealed class GameSettings
	{
		/// <summary>
		/// The difficulty of the game.
		/// </summary>
		[WireMember(1)]
		public DifficultyType Difficulty { get; internal set; }

		/// <summary>
		/// Indicates if it is battlemode.
		/// </summary>
		[WireMember(2)]
		public bool isBattle { get; internal set; }

		//TODO: What is event?
		[WireMember(3)]
		public byte Event { get; internal set; }

		/// <summary>
		/// The section id associated with the game.
		/// </summary>
		[WireMember(4)]
		public SectionId Section { get; internal set; }

		/// <summary>
		/// Indicates if the game is challenge mode.
		/// </summary>
		[WireMember(5)]
		public bool isChallengeMode { get; internal set; }

		/// <summary>
		/// The random seed associated with the room.
		/// </summary>
		[WireMember(6)]
		public uint RandomSeed { get; internal set; }

		/// <summary>
		/// The episode that the game is in.
		/// </summary>
		[WireMember(7)]
		public EpisodeType Episode { get; internal set; }

		[WireMember(8)]
		internal byte One { get; set; } = 1;

		/// <summary>
		/// Indicates if the game is single-player.
		/// </summary>
		[WireMember(9)]
		public bool isSinglePlayer { get; internal set; }

		//TODO: Are we use this is unused?
		[WireMember(10)]
		internal byte unused { get; set; }

		public GameSettings(DifficultyType difficulty, 
			bool isBattle, 
			byte @event, 
			SectionId section, 
			bool isChallengeMode, 
			uint randomSeed, 
			EpisodeType episode, bool isSinglePlayer)
		{
			Difficulty = difficulty;
			this.isBattle = isBattle;
			Event = @event;
			Section = section;
			this.isChallengeMode = isChallengeMode;
			RandomSeed = randomSeed;
			Episode = episode;
			this.isSinglePlayer = isSinglePlayer;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public GameSettings()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"Mode: {Difficulty} Episode: {Episode} Battle: {isBattle} Challenge: {isChallengeMode} Single: {isSinglePlayer} SectionId: {Section}";
		}
	}
}
