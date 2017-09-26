using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// The base type for PSOBB patching packet payloads that the server sends. This isn't for ship payloads. The child
	/// type based on a 2 byte opcode <see cref="ushort"/> that comes over the network.
	/// </summary>
	[WireDataContract(WireDataContractAttribute.KeyType.UShort, true)]
	public abstract class PSOBBPatchPacketPayloadServer
	{
		//Nothing, only the 2 byte Type is relevant for this base packet.

		protected PSOBBPatchPacketPayloadServer()
		{
			
		}
	}
}
