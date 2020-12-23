using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using GladNet;

namespace Booma.Proxy
{
	/// <summary>
	/// The base type for PSOBB patching packet payloads that the server sends. This isn't for ship payloads. The child
	/// type based on a 2 byte opcode <see cref="ushort"/> that comes over the network.
	/// </summary>
	[DefaultChild(typeof(UnknownPatchPayload))] //this will be the default deserialized packet when we don't know what it is.
	[WireDataContract(PrimitiveSizeType.UInt16)]
	public abstract partial class PSOBBPatchPacketPayloadServer : IPacketPayload, IOperationCodeable<PatchNetworkOperationCode>
	{
		//We really only add this because sometimes we'll get a packet we don't know about and we'll want to log about it.
		/// <summary>
		/// The operation code of the packet.
		/// </summary>
		[WireMember(1)]
		public PatchNetworkOperationCode OperationCode { get; internal set; }

		//Nothing, only the 2 byte Type is relevant for this base packet.
		protected PSOBBPatchPacketPayloadServer(PatchNetworkOperationCode operationCode)
		{
			//This is in a serialization hotpath so we don't verify the enum with
			//and throw because it depends on slow reflection.
			OperationCode = operationCode;
		}
	}
}
