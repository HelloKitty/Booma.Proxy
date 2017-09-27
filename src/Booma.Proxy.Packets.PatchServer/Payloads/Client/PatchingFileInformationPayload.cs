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
	[PatchServerPacketPayload(PatchNetworkOperationCodes.PATCH_SEND_INFO)]
	public sealed class PatchingFileInformationPayload : PSOBBPatchPacketPayloadClient
	{
		// index
		// checksum
		// size

		/// <summary>
		/// Patch file index (or id, however you want to see it)
		/// </summary>
		[WireMember(1)]
		public int PatchFileIndex { get; }

		/// <summary>
		/// Patch file checksum
		/// </summary>
		[WireMember(2)]
		public uint PatchFileChecksum { get; }

		/// <summary>
		/// Patch file size
		/// </summary>
		[WireMember(3)]
		public int PatchFileSize { get; }

		public PatchingFileInformationPayload(int patchFileIndex, uint patchFileChecksum, int patchFileSize)
		{
			if (patchFileIndex < 0) throw new ArgumentOutOfRangeException(nameof(patchFileIndex));
			if (patchFileSize < 0) throw new ArgumentOutOfRangeException(nameof(patchFileSize));

			PatchFileIndex = patchFileIndex;
			PatchFileChecksum = patchFileChecksum;
			PatchFileSize = patchFileSize;
		}

		//Serializer ctor
		protected PatchingFileInformationPayload()
			: base()
		{

		}
	}
}
