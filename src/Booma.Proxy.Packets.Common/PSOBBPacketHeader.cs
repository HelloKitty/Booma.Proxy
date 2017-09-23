using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// The header for PSOBB packets.
	/// This looks non-standard according to documentation but for serialization purposes it's easier to
	/// write 
	/// </summary>
	[WireDataContract]
	public class PSOBBPacketHeader
	{
		/// <summary>
		/// The size of the packet.
		/// </summary>
		[WireMember(1)]
		public short PacketSize { get; }

		/// <summary>
		/// Indicates the size of the payload.
		/// </summary>
		public int PayloadSize => PacketSize - sizeof(short);
	}
}
