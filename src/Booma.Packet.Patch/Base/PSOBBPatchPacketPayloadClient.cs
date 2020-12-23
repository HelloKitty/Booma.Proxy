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
	/// The base type for PSOBB patching packet payloads that the client sends. This isn't for ship payloads The child
	/// type based on a 2 byte opcode <see cref="ushort"/> that comes over the network.
	/// </summary>
	[WireDataContract(PrimitiveSizeType.UInt16)]
	public abstract class PSOBBPatchPacketPayloadClient : IPacketPayload, IOperationCodeable<PatchNetworkOperationCode>
	{
		//Nothing, only the 2 byte Type is relevant for this base packet.
		/// <inheritdoc />
		[WireMember(1)]
		public PatchNetworkOperationCode OperationCode { get; internal set; }

		protected PSOBBPatchPacketPayloadClient(PatchNetworkOperationCode operationCode)
		{
			//This is in a serialization hotpath so we don't verify the enum with
			//and throw because it depends on slow reflection.
			OperationCode = operationCode;
		}
	}
}
