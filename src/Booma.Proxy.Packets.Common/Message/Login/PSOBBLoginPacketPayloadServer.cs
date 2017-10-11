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
	public abstract class PSOBBLoginPacketPayloadServer : IPacketPayload
	{
		//We really only add this because sometimes we'll get a packet we don't know about and we'll want to log about it.
		/// <summary>
		/// The operation code of the packet.
		/// </summary>
		[DontWrite] //we don't want to write this since the type key already handlers opcodes
		[WireMember(1)]
		protected short OperationCode { get; }

		/// <summary>
		/// Indicates if the flags is serialized with <see cref="Flags"/>.
		/// If false then serialization for <see cref="Flags"/> will be skipped
		/// meaning the 4 bytes can be consumed by an inheriting class instead for
		/// other data by the serializer.
		/// Default: True
		/// </summary>
		public virtual bool isFlagsSerialized => true;

		/// <summary>
		/// The optional flags field.
		/// This value is different for some packets than others.
		/// </summary>
		[Optional(nameof(isFlagsSerialized))] //Makes this flags optional; some subpayloads may want to consume the 4 bytes instead
		[KnownSize(4)] //always 4 bytes
		[WireMember(2)]
		private byte[] Flags { get; } = new byte[4]; //we can initialize new flags every payload since they're always there

		protected PSOBBLoginPacketPayloadServer()
		{
			
		}
	}
}
