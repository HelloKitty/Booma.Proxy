using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace Booma.Proxy
{
	/// <summary>
	/// The header for PSOBB packets.
	/// This looks non-standard according to documentation but for serialization purposes it's easier to
	/// write 
	/// </summary>
	[WireDataContract]
	public class PSOBBPacketHeader : IPacketHeader
	{
		/// <summary>
		/// The size of the packet.
		/// </summary>
		[WireMember(1)]
		internal short _PacketSize { get; set; }

		/// <inheritdoc />
		public int PacketSize => _PacketSize;

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
			_PacketSize = packetSize;
		}

		/// <summary>
		/// Creates a new packet header with the specified size.
		/// </summary>
		/// <param name="packetSize">The packet size</param>
		public PSOBBPacketHeader(int packetSize)
		{
			_PacketSize = (short)packetSize;
		}

		//serializer ctor
		private PSOBBPacketHeader()
		{
			
		}
	}
}
