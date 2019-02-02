namespace Booma.Proxy
{
	/// <summary>
	/// Enumeration of scene types.
	/// </summary>
	public enum GameSceneType
	{
		LobbyDefault = 1,

		LobbySoccer = 2,

		Pioneer2 = 3,

		RagolDefault = 4,

		TitleScreen = 5,

		CharacterSelectionScreen = 6,

		ServerSelectionScreen = 7,

		/// <summary>
		/// This is the scene right before bursting into the block's lobby.
		/// </summary>
		PreBlockBurstingScene = 8,

		/// <summary>
		/// The scene right before ship selection, that looks liks bursting.
		/// </summary>
		PreShipSelectionScene = 9,
	}
}