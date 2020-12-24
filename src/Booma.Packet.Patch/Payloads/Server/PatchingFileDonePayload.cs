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
	public sealed partial class PatchingFileDonePayload : PSOBBPatchPacketPayloadServer
	{
		/// <summary>
		/// Padding
		/// </summary>
		[WireMember(1)]
		public int Padding { get; internal set; }

		public PatchingFileDonePayload()
			: base(PatchNetworkOperationCode.PATCH_FILE_DONE)
		{

		}
	}
}
