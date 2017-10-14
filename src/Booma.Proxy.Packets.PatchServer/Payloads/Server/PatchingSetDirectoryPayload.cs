using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Sets the directory to move into, creating it if it doesn't exist
	/// </summary>
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCode.PATCH_SET_DIRECTORY)]
	public sealed class PatchingSetDirectoryPayload : PSOBBPatchPacketPayloadServer
	{
		/// <summary>
		/// Patch file index
		/// </summary>
		[KnownSize(64)]
		[WireMember(1)]
		public string PatchDirectoryname { get; }

		public PatchingSetDirectoryPayload(string patchDirectoryName)
		{
			if (string.IsNullOrWhiteSpace(patchDirectoryName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(patchDirectoryName));
			if (patchDirectoryName.Length > 64) throw new ArgumentException("Directory name cannot be longer than 64 characters", nameof(patchDirectoryName));

			PatchDirectoryname = patchDirectoryName;
		}

		//Serializer ctor
		private PatchingSetDirectoryPayload()
		{

		}
	}
}
