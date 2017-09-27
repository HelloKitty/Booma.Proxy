using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy.Packets.PatchServer.Payloads.Client
{
	/// <summary>
	/// Client file information report
	/// The server decides to patch a file based on this data
	/// </summary>
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCodes.PATCH_FILE_LIST_DONE)]
	public sealed class PatchingFileInformationDoneAckPayload : PSOBBPatchPacketPayloadClient
	{
		public PatchingFileInformationDoneAckPayload()
		{
		}
	}
}
