namespace Booma.Proxy
{
	//Syl: https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/packets.h#L1710
	/// <summary>
	/// Enumeration of login response codes.
	/// </summary>
	public enum LoginResponseCode : int
	{
		LOGIN_93BB_OK = 0,
		LOGIN_93BB_UNKNOWN_ERROR = 1,
		LOGIN_93BB_BAD_USER_PWD = 2,
		LOGIN_93BB_BAD_USER_PWD2 = 3,
		LOGIN_93BB_MAINTENANCE = 4,
		LOGIN_93BB_ALREADY_ONLINE5,
		LOGIN_93BB_BANNED = 6,
		LOGIN_93BB_BANNED2 = 7,
		LOGIN_93BB_NO_USER_RECORD = 8,
		LOGIN_93BB_PAY_UP = 9,
		LOGIN_93BB_LOCKED = 10,  /* Improper shutdown */
		LOGIN_93BB_BAD_VERSION = 11,
		LOGIN_93BB_FORCED_DISCONNECT12 = 12,
	}
}