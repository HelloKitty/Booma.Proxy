using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//0x04 0x00 0x12 0x00
	//Syl: PATCH_SEND_DONE https://github.com/Sylverant/patch_server/blob/1616d93cc653703e3787c246dfb7aaa8ef3044b1/src/patch_server.c#L512
	/// <summary>
	/// Patching command that indicates that the patching is done.
	/// </summary>
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCode.PATCH_SEND_DONE)]
	public sealed partial class PatchingDoneCommandPayload : PSOBBPatchPacketPayloadServer
	{
		/// <summary>
		/// Empty command packet that indicates patching is finished.
		/// </summary>
		public PatchingDoneCommandPayload()
			: base(PatchNetworkOperationCode.PATCH_SEND_DONE)
		{
			
		}
	}
}
