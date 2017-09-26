using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// The base type for PSOBB login payloads that the client sends. This isn't for patch/ship.
	/// Contains the <see cref="Flags"/> optional byte chunk and maps to child
	/// types based on a 2 byte opcode <see cref="ushort"/> that comes over the network.
	/// </summary>
	[DefaultChild(typeof(UnknownLoginPacket))] //this will be the default deserialized packet when we don't know what it is.
	[WireDataContract(WireDataContractAttribute.KeyType.UShort, InformationHandlingFlags.DontConsumeRead, true)]
	public abstract class PSOBBLoginPacketPayloadServer
	{
		//We really only add this because sometimes we'll get a packet we don't know about and we'll want to log about it.
		/// <summary>
		/// The operation code of the packet.
		/// </summary>
		[WireMember(1)]
		protected short OperationCode { get; }

		/// <summary>
		/// The optional flags field.
		/// This value is different for some packets than others.
		/// </summary>
		[KnownSize(4)] //always 4 bytes
		[WireMember(1)]
		public byte[] Flags { get; } = new byte[4]; //we can initialize new flags every payload since they're always there
	}
}
