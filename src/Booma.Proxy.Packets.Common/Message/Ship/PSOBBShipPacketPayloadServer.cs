using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// The base type for PSOBB ship packet payloads that the server sends. This isn't for patch/login.
	/// Contains the <see cref="Flags"/> optional byte chunk and maps to child
	/// types based on a 2 byte opcode <see cref="ushort"/> that comes over the network.
	/// </summary>
	[WireDataContract(WireDataContractAttribute.KeyType.UShort, true)]
	public abstract class PSOBBShipPacketPayloadServer : IPacketPayload
	{
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
		[WireMember(1)]
		private byte[] Flags { get; } = new byte[4]; //we can initialize new flags every payload since they're always there

		protected PSOBBShipPacketPayloadServer()
		{
			
		}
	}
}
