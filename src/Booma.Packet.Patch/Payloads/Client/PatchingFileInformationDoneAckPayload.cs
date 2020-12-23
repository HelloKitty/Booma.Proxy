using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//TODO: Document this please, idk what it is
	[WireDataContract]
	[PatchClientPacketPayload(PatchNetworkOperationCode.PATCH_FILE_LIST_DONE)]
	public sealed class PatchingFileInformationDoneAckPayload : PSOBBPatchPacketPayloadClient
	{
		public PatchingFileInformationDoneAckPayload()
			: base(PatchNetworkOperationCode.PATCH_FILE_LIST_DONE)
		{
		}
	}
}
