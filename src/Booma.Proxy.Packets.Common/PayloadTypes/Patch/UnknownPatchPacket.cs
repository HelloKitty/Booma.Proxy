using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// The default/unknown packet that is deserialized when an unknown
	/// or unimplemented opcode is encountered.
	/// </summary>
	public class UnknownPatchPacket : PSOBBPatchPacketPayloadServer
	{
		//We don't know what the packet is so we can't put any information here
		/// <summary>
		/// The entire unknown deserialized bytes for login packets.
		/// </summary>
		[ReadToEnd]
		[WireMember(1)]
		public byte[] UnknownBytes { get; }

		//Serializer ctor
		private UnknownPatchPacket()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"Unknown OpCode: #{OperationCode:X} Type: {base.ToString()} Size: {UnknownBytes.Length + 2}";
		}
	}
}
