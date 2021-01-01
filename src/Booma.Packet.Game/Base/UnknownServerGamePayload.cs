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
	public partial class UnknownServerGamePayload : PSOBBGamePacketPayloadServer, IUnknownPayloadType
	{
		//For unknown payloads we don't want to serialize the flags so that all bytes end up in the UnknownBytes property
		/// <inheritdoc />
		public override bool isFlagsSerialized { get; } = false;

		/// <summary>
		/// The entire unknown deserialized bytes for game packets.
		/// </summary>
		[ReadToEnd]
		[WireMember(1)]
		public byte[] UnknownBytes { get; internal set; }

		/// <summary>
		/// Creates a new unknown payload with the provided <see cref="operationCode"/>.
		/// and the binary buffer <see cref="bytes"/>
		/// </summary>
		/// <param name="operationCode"></param>
		/// <param name="bytes"></param>
		public UnknownServerGamePayload(GameNetworkOperationCode operationCode, byte[] bytes) 
			: base(operationCode)
		{
			UnknownBytes = bytes;
		}

		/// <summary>
		/// Serializer ctor
		/// </summary>
		public UnknownServerGamePayload()
			: base(GameNetworkOperationCode.UNKNOWN)
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
