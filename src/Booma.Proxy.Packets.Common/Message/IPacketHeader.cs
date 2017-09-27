using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for PSOBB packet headers.
	/// </summary>
	public interface IPacketHeader
	{
		/// <summary>
		/// The size of the packet.
		/// </summary>
		short PacketSize { get; }

		//The PacketSize contains the whole size of the packet
		//So the payload is just the size minus the size of the packetsize field.
		/// <summary>
		/// Indicates the size of the payload.
		/// </summary>
		int PayloadSize { get; }
	}
}
