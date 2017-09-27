using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCodes.PATCH_INFO_FINISHED)]
	public sealed class PatchingInfoRequestDonePayload : PSOBBPatchPacketPayloadServer
	{
		public PatchingInfoRequestDonePayload()
		{

		}
	}
}
