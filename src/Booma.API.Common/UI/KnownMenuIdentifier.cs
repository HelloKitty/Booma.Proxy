using System;
using System.Collections.Generic;
using System.Text;

namespace Booma.UI
{
	//See: https://github.com/Sylverant/ship_server/blob/83d06b342c2629fc6812ed49000163e95b1a5fb7/src/ship_packets.h#L61
	/// <summary>
	/// Enumeration of known menu identifiers.
	/// </summary>
	public enum KnownMenuIdentifier : uint
	{
		INFODESK = 0x00000000,
		BLOCK = 0x00000001,
		GAME = 0x00000002,
		QCATEGORY = 0x00000003,
		QUEST = 0x00000004,
		SHIP = 0x00000005,
		GAME_TYPE = 0x00000006,
		GM = 0x00000007,
		LOBBY = 0xFFFFFFFF
	}
}
