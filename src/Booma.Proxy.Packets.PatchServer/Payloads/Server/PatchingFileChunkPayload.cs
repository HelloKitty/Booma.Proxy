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
	[PatchServerPacketPayload(PatchNetworkOperationCodes.PATCH_DATA_SEND)]
	public sealed class PatchingFileChunkPayload : PSOBBPatchPacketPayloadServer
	{
		/// <summary>
		/// Patch file chunk index
		/// </summary>
		[WireMember(1)]
		public int PatchFileChunkIndex { get; }
		
		/// <summary>
		/// Patch file chunk checksum (CRC32)
		/// </summary>
		[WireMember(2)]
		public uint PatchFileChunkChecksum { get; }

		// Chunk size is part of the packet, not used because of FreecraftCore

		/// <summary>
		/// Patch file chunk data
		/// </summary>
		[SendSize(SendSizeAttribute.SizeType.Int32)]
		[WireMember(3)]
		public byte[] PatchFileChunkData { get; }

		public PatchingFileChunkPayload(int patchFileChunkIndex, uint patchFileChunkChecksum, byte[] patchFileChunkData)
		{
			if (patchFileChunkIndex < 0) throw new ArgumentOutOfRangeException(nameof(patchFileChunkIndex));
			if (patchFileChunkData == null) throw new ArgumentNullException(nameof(patchFileChunkData));
			if (patchFileChunkData.Length >= 0x6000) throw new ArgumentException("Chunk length cannot be longer than 24576 (0x6000) bytes", nameof(patchFileChunkData));

			PatchFileChunkIndex = patchFileChunkIndex;
			PatchFileChunkChecksum = patchFileChunkChecksum;
			PatchFileChunkData = patchFileChunkData;
		}

		//Serializer ctor
		protected PatchingFileChunkPayload()
			: base()
		{

		}
	}
}
