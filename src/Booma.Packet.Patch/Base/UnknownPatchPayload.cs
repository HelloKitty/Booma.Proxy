using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// The default/unknown packet that is deserialized when an unknown
	/// or unimplemented opcode is encountered.
	/// </summary>
	[WireDataContract]
	public partial class UnknownPatchPayload : PSOBBPatchPacketPayloadServer, IUnknownPayloadType
	{
		//We don't know what the packet is so we can't put any information here
		/// <summary>
		/// The entire unknown deserialized bytes for login packets.
		/// </summary>
		[ReadToEnd]
		[WireMember(1)]
		public byte[] UnknownBytes { get; internal set; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public UnknownPatchPayload()
			: base(PatchNetworkOperationCode.PATCH_UNKNOWN)
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"Unknown OpCode: {OperationCode:X} Type: {base.ToString()} Size: {UnknownBytes.Length + 2}";
		}
	}
}
