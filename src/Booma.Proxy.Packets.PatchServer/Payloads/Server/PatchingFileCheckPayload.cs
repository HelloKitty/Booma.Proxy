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
	[PatchServerPacketPayload(PatchNetworkOperationCodes.PATCH_FILE_SEND)]
	public sealed class PatchingFileCheckPayload : PSOBBPatchPacketPayloadServer
	{
		/// <summary>
		/// Patch file index
		/// </summary>
		[WireMember(1)]
		public int PatchFileIndex { get; }

		/// <summary>
		/// Patch file name
		/// </summary>
		[KnownSize(48)]
		[WireMember(2)]
		public string PatchFileName { get; }

		public PatchingFileCheckPayload(int patchFileIndex, string patchFileName)
		{
			if (patchFileIndex < 0) throw new ArgumentOutOfRangeException(nameof(patchFileIndex));
			if (string.IsNullOrWhiteSpace(patchFileName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(patchFileName));
			if (patchFileName.Length > 32) throw new ArgumentException("File name cannot be longer than 32 characters", nameof(patchFileName));

			PatchFileIndex = patchFileIndex;
			PatchFileName = patchFileName;
		}

		//Serializer ctor
		protected PatchingFileCheckPayload()
			: base()
		{

		}
	}
}
