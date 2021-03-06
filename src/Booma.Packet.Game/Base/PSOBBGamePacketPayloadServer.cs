using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// The base type for PSOBB Game payloads that the client sends. This isn't for patch server.
	/// Contains the <see cref="Flags"/> optional byte chunk and maps to child
	/// types based on a 2 byte opcode <see cref="ushort"/> that comes over the network.
	/// </summary>
	[WireMessageType]
	[DefaultChild(typeof(UnknownServerGamePayload))] //this will be the default deserialized packet when we don't know what it is.
	[WireDataContract(PrimitiveSizeType.UInt16)]
	public abstract partial class PSOBBGamePacketPayloadServer : IOperationCodeable<GameNetworkOperationCode>
	{
		//We really only add this because sometimes we'll get a packet we don't know about and we'll want to log about it.
		/// <summary>
		/// The operation code of the packet.
		/// </summary>
		[WireMember(1)]
		public GameNetworkOperationCode OperationCode { get; internal set; }

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
		internal byte[] Flags { get; set; } = Array.Empty<byte>(); //we can initialize new flags every payload since they're always there

		protected PSOBBGamePacketPayloadServer(GameNetworkOperationCode operationCode)
		{
			//This is in a serialization hotpath so we don't verify the enum with
			//and throw because it depends on slow reflection.
			OperationCode = operationCode;
		}
	}
}
