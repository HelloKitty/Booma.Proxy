using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy.Packets.PatchServer.Payloads.Server
{
	// This packet is sent once when starting file checks 
	// and again when starting the update process which
	// leads me to think this is a "go to root" packet.
	// Will confirm or deny later after in the game.
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCodes.PATCH_START_LIST)]
	public sealed class PatchingStartPatchPayload : PSOBBPatchPacketPayloadServer
	{
		public PatchingStartPatchPayload()
		{
			
		}
	}
}
