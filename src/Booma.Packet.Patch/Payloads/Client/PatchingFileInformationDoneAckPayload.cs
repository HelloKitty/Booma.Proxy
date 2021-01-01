using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	//TODO: Document this please, idk what it is
	[WireDataContract]
	[PatchClientPacketPayload(PatchNetworkOperationCode.PATCH_FILE_LIST_DONE)]
	public sealed partial class PatchingFileInformationDoneAckPayload : PSOBBPatchPacketPayloadClient
	{
		public PatchingFileInformationDoneAckPayload()
			: base(PatchNetworkOperationCode.PATCH_FILE_LIST_DONE)
		{
		}
	}
}
