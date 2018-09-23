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
	public class UnknownClientGamePayload : PSOBBGamePacketPayloadClient, IUnknownPayloadType
	{
		//For unknown payloads we don't want to serialize the flags so that all bytes end up in the UnknownBytes property
		/// <inheritdoc />
		public override bool isFlagsSerialized => false;

		//We don't know what the packet is so we can't put any information here
		/// <summary>
		/// The entire unknown deserialized bytes for game packets.
		/// </summary>
		[ReadToEnd]
		[WireMember(1)]
		public byte[] UnknownBytes { get; }

		//Serializer ctor
		private UnknownClientGamePayload()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			if(Enum.IsDefined(typeof(GameNetworkOperationCode), OperationCode))
				return $"Unknown OpCode: {OperationCode:X} Name: {((GameNetworkOperationCode)OperationCode).ToString()} Type: {base.ToString()} Size: {4 + UnknownBytes.Length + 2}";
			else
				return $"Unknown OpCode: {OperationCode:X} Type: {base.ToString()} Size: {4 + UnknownBytes.Length + 2}";
		}
	}
}
