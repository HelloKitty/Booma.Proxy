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
		
		//The PacketSize contains the whole size of the packet
		//So the payload is just the size minus the size of the packetsize field.
		/// <summary>
		/// Indicates the size of the payload.
		/// </summary>
		public int PayloadSize => PacketSize - sizeof(short);

		/// <summary>
		/// Creates a new packet header with the specified size.
		/// </summary>
		/// <param name="packetSize">The packet size</param>
		public PSOBBPacketHeader(short packetSize)
		{
			PacketSize = packetSize;
		}

		/// <summary>
		/// Creates a new packet header with the specified size.
		/// </summary>
		/// <param name="packetSize">The packet size</param>
		public PSOBBPacketHeader(int packetSize)
		{
			PacketSize = (short)packetSize;
		}

		//serializer ctor
		private PSOBBPacketHeader()
		{
			
		}
	}
}
