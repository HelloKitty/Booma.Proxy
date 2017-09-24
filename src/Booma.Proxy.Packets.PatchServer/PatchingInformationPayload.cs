using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//Empty from Sodaboy's proxy
	/*unsigned char patch_pkt[] = { 
	0x0C, 0x00, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00,
	0x0A, 0x00, 0x04, 0x00, 0x12, 0x00
	};*/

	//Tethella implementation: https://github.com/justnoxx/psobb-tethealla/blob/master/patch_server/patch_server.c#L578
	//Sylverant implementation: https://github.com/Sylverant/patch_server/blob/master/src/patch_packets.c#L237 and structure https://github.com/Sylverant/patch_server/blob/master/src/patch_packets.h#L106
	[WireDataContract]
	[WireDataContractBaseTypeRuntimeLink(0x11)] //TODO: Make an operations code enum.
	public sealed class PatchingInformationPayload : PSOBBPatchPacketPayload
	{
		//0x0C 0x00 Size
		//0x11 0x00 Type
		//0x00 0x00 0x00 0x00 Optional Flags

		//If there is patching information it will send
		/// <summary>
		/// Indicates the length and size of the patching data.
		/// </summary>
		[WireMember(1)]
		public int PatchingByteLength { get; }

		/// <summary>
		/// Not 100% sure but looks like the number of files that need to be patched.
		/// </summary>
		[WireMember(2)]
		public int PatchFileCount { get; }

		public PatchingInformationPayload(int patchingByteLength, int patchFileCount)
		{
			if(patchingByteLength < 0) throw new ArgumentOutOfRangeException(nameof(patchingByteLength));
			if(patchFileCount < 0) throw new ArgumentOutOfRangeException(nameof(patchFileCount));

			PatchingByteLength = patchingByteLength;
			PatchFileCount = patchFileCount;
		}

		//Serializer ctor
		private PatchingInformationPayload()
		{
			
		}
	}
}
