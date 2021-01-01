using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Enumeration of sub-operation codes for 0x6D packet.
	/// </summary>
	public enum SubCommand6DOperationCode : byte //it's sent as byte from server
	{
		Unknown = 0x00,
		//TODO: Is this a good name? This one is sent about a player to a joining player, not just the leader.
		PlayerJoinedData = 0x70,
	}
}
