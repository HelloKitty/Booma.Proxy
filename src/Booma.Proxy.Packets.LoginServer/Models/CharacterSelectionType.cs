using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	//Based off of syl information here: https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/bbcharacter.c#L425
	/// <summary>
	/// Enumeration of character selection types.
	/// </summary>
	public enum CharacterSelectionType : byte
	{
		/// <summary>
		/// The selection is for a preview for the character.
		/// </summary>
		Preview = 0,

		/// <summary>
		/// The selection is for picking/playing.
		/// </summary>
		PlaySelection = 1 //Any value other than 0 will work
	}
}
