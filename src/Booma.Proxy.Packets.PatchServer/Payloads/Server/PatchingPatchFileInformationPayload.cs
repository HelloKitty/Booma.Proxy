using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//0x0C 0x00 0x11 0x00
	//PatchingByteLength{4} PatchFileCount{4}
	//Tethella implementation: https://github.com/justnoxx/psobb-tethealla/blob/master/patch_server/patch_server.c#L578
	//Sylverant implementation: https://github.com/Sylverant/patch_server/blob/master/src/patch_packets.c#L237 and structure https://github.com/Sylverant/patch_server/blob/master/src/patch_packets.h#L106
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCodes.PATCH_FILE_SEND)]
	public sealed class PatchingUpdateFileInformationPayload : PSOBBPatchPacketPayloadServer
	{
		/// <summary>
		/// Patch file index
		/// </summary>
		[WireMember(1)]
		public int PatchFileIndex { get; }

		/// <summary>
		/// Patch file size
		/// </summary>
		[WireMember(2)]
		public int PatchFileSize { get; }

		/// <summary>
		/// Patch file name
		/// </summary>
		[KnownSize(48)]
		[WireMember(3)]
		public string PatchFileName { get; }

		public PatchingUpdateFileInformationPayload(int patchFileIndex, int patchFileSize, string patchFileName)
		{
			if (patchFileIndex < 0) throw new ArgumentOutOfRangeException(nameof(patchFileIndex));
			if (patchFileSize < 0) throw new ArgumentOutOfRangeException(nameof(patchFileSize));
			if (string.IsNullOrWhiteSpace(patchFileName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(patchFileName));

			PatchFileIndex = patchFileIndex;
			PatchFileSize = patchFileSize;
			PatchFileName = patchFileName;
		}

		//Serializer ctor
		protected PatchingUpdateFileInformationPayload()
			: base()
		{

		}
	}
}
