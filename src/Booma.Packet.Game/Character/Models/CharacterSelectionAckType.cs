namespace Booma
{
	/// <summary>
	/// https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/packets.h#L1729
	/// </summary>
	public enum CharacterSelectionAckType : int
	{
		BB_CHAR_ACK_UPDATE = 0,

		BB_CHAR_ACK_SELECT = 1,

		BB_CHAR_ACK_NONEXISTANT = 2
	}
}