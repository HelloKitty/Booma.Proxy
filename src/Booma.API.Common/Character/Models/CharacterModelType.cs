using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Enumeration of all character model types.
	/// (Ex. Regular, Sonic, Tails, Flowen)
	/// </summary>
	public enum CharacterModelType : byte
	{
		/// <summary>
		/// The regular character will be shown.
		/// </summary>
		Regular = 0,

		//TODO: Are these right?
		Famitsu,
		Rico,
		Sonic,
		Knuckles,
		Tails,
		Flowen,
		Elly,
		Momoka,
		Irene,
		GuildLady,
		Nurse,

		//TODO: Fill in when other NPC ids are known.
	}
}
