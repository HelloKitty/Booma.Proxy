using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Enumeration of sub-operation codes for 0x62 packet
	/// <see cref="BlockNetworkCommand62EventClientPayload"/> and ... TODO server
	/// </summary>
	public enum SubCommand62OperationCode : byte //it's sent as byte from server
	{
		PhotonChairCommand = 0xAE
	}
}
