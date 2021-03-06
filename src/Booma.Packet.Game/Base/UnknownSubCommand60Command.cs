using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// An unimplemented or unknown subcommand for the <see cref="BlockNetworkCommand60EventClientPayload"/>.
	/// </summary>
	[WireDataContract]
	public partial class UnknownSubCommand60Command : BaseSubCommand60, IUnknownPayloadType
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
		public UnknownSubCommand60Command()
			: base(SubCommand60OperationCode.Unknown)
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			if(Enum.IsDefined(typeof(SubCommand60OperationCode), (byte)OperationCode))
				return $"Unknown Subcommand60 OpCode: {OperationCode:X} Name: {((SubCommand60OperationCode)OperationCode).ToString()} Type: {GetType().Name} CommandSize: {CommandSize * 4 - 2} (bytes size)";
			else
				return $"Unknown Subcommand60 OpCode: {OperationCode:X} Type: {GetType().Name} CommandSize: {CommandSize * 4 - 2} (bytes size)";
		}
	}
}
