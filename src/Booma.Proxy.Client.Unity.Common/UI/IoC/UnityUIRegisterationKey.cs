using System;
using System.Collections.Generic;
using System.Text;

namespace Guardians
{
	/// <summary>
	/// Enumeration of keys for Unity UI
	/// IoC container registeration.
	/// </summary>
	public enum UnityUIRegisterationKey
	{
		//I know it's weird, but I did character screen first. That's why they're first
		Unknown = 0,

		CharacterSlot1 = 1,

		CharacterSlot2 = 2,

		CharacterSlot3 = 3,

		CharacterSlot4 = 4,

		TitleLoginUsername = 5,

		TitleLoginPassword = 6,

		TitleLoginButton = 7,

		Marquee = 8
	}
}
