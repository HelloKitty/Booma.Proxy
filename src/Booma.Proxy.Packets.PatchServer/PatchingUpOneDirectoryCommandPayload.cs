using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{

	//Syl: PATCH_ONE_DIR_UP https://github.com/Sylverant/patch_server/blob/1616d93cc653703e3787c246dfb7aaa8ef3044b1/src/patch_server.c#L207
	[WireDataContract]
	[WireDataContractBaseTypeRuntimeLink(0x0A)] //TODO: Enumerate opcodes
	public sealed class PatchingUpOneDirectoryCommandPayload : PSOBBPatchPacketPayload
	{
		//Empty command packet that:
		//Syl: "If t1 is non-NULL, we need to go up the tree as many times as we have
		//path components left to be parsed."
	}
}
