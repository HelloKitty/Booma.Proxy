using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// An unimplemented or unknown subcommand for the 0x6D packets.
	/// </summary>
	[WireDataContract]
	public partial class UnknownSubCommand6DCommand : BaseSubCommand6D, IUnknownPayloadType
	{
		/// <inheritdoc />
		public short OperationCode => (short)base.CommandOperationCode;

		/// <inheritdoc />
		[ReadToEnd]
		[WireMember(1)]
		public byte[] UnknownBytes { get; internal set; } = Array.Empty<byte>(); //readtoend requires at least an empty array init

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public UnknownSubCommand6DCommand()
			: base(SubCommand6DOperationCode.Unknown)
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			if(Enum.IsDefined(typeof(SubCommand6DOperationCode), (byte)OperationCode))
				return $"Unknown Subcommand6D OpCode: {OperationCode:X} Name: {((SubCommand6DOperationCode)OperationCode).ToString()} Type: {GetType().Name} CommandSize: {CommandSize} (bytes size)";
			else
				return $"Unknown Subcommand6D OpCode: {OperationCode:X} Type: {GetType().Name} CommandSize: {CommandSize} (bytes size)";
		}
	}
}
