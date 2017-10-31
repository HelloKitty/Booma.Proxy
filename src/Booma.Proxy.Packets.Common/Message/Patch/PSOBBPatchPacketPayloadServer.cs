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
	[WireDataContract(WireDataContractAttribute.KeyType.UShort, InformationHandlingFlags.DontConsumeRead, true)]
	public abstract class PSOBBPatchPacketPayloadServer : IPacketPayload
	{
		//We really only add this because sometimes we'll get a packet we don't know about and we'll want to log about it.
		/// <summary>
		/// The operation code of the packet.
		/// </summary>
		[DontWrite] //we don't want to write this since the type key already handlers opcodes
		[WireMember(1)]
		protected short OperationCode { get; }

		//Nothing, only the 2 byte Type is relevant for this base packet.

		protected PSOBBPatchPacketPayloadServer()
		{
			
		}
	}
}
