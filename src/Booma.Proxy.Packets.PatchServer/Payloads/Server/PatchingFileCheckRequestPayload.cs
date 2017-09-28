using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Requests a file check for updating
	/// </summary>
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCodes.PATCH_FILE_INFO)]
	public sealed class PatchingFileCheckRequestPayload : PSOBBPatchPacketPayloadServer
	{
		/// <summary>
		/// Patch file index
		/// </summary>
		[WireMember(1)]
		public int PatchFileIndex { get; }

		/// <summary>
		/// Patch file name
		/// </summary>
		[KnownSize(32)]
		[WireMember(2)]
		public string PatchFileName { get; }

		public PatchingFileCheckRequestPayload(int patchFileIndex, string patchFileName)
		{
			if (patchFileIndex < 0) throw new ArgumentOutOfRangeException(nameof(patchFileIndex));
			if (string.IsNullOrWhiteSpace(patchFileName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(patchFileName));
			if (patchFileName.Length > 32) throw new ArgumentException("File name cannot be longer than 32 characters", nameof(patchFileName));

			PatchFileIndex = patchFileIndex;
			PatchFileName = patchFileName;
		}

		//Serializer ctor
		private PatchingFileCheckRequestPayload()
		{

		}
	}
}
