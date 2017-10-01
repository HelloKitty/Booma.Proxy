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
	public class UnknownLoginPacket : PSOBBLoginPacketPayloadServer, IUnknownPayloadType
	{
		//We don't know what the packet is so we can't put any information here

		/// <inheritdoc />
		public new short OperationCode => base.OperationCode;

		/// <summary>
		/// The entire unknown deserialized bytes for login packets.
		/// </summary>
		[ReadToEnd]
		[WireMember(1)]
		public byte[] UnknownBytes { get; }

		//Serializer ctor
		private UnknownLoginPacket()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"Unknown OpCode: #{OperationCode:X} Type: {base.ToString()} Size: {Flags.Length + UnknownBytes.Length + 2}";
		}
	}
}
