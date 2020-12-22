using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCode.PATCH_FILE_DONE)]
	public sealed class PatchingFileDonePayload : PSOBBPatchPacketPayloadServer
	{
		/// <summary>
		/// Padding
		/// </summary>
		[WireMember(1)]
		public int Padding { get; }

		public PatchingFileDonePayload()
		{
		}
	}
}
