using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Sets the file we are going to update
	/// </summary>
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCode.PATCH_FILE_SEND)]
	public sealed class PatchingFilePatchPayload : PSOBBPatchPacketPayloadServer
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

		public PatchingFilePatchPayload(int patchFileIndex, int patchFileSize, string patchFileName)
		{
			if (patchFileIndex < 0) throw new ArgumentOutOfRangeException(nameof(patchFileIndex));
			if (patchFileSize < 0) throw new ArgumentOutOfRangeException(nameof(patchFileSize));
			if (string.IsNullOrWhiteSpace(patchFileName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(patchFileName));
			if (patchFileName.Length > 48) throw new ArgumentException("File name cannot be longer than 48 characters", nameof(patchFileName));

			PatchFileIndex = patchFileIndex;
			PatchFileSize = patchFileSize;
			PatchFileName = patchFileName;
		}

		//Serializer ctor
		private PatchingFilePatchPayload()
		{

		}
	}
}
