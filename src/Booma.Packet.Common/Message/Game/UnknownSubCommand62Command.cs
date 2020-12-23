using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// An unimplemented or unknown subcommand for the <see cref="BlockNetworkCommand62EventClientPayload"/>.
	/// </summary>
	[WireDataContract]
	public sealed class UnknownSubCommand62Command : BaseSubCommand62, IUnknownPayloadType
	{
		/// <inheritdoc />
		public short OperationCode => (short)base.CommandOperationCode;

		/// <inheritdoc />
		[ReadToEnd]
		[WireMember(1)]
		public byte[] UnknownBytes { get; internal set; } = new byte[0]; //readtoend requires at least an empty array init

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public UnknownSubCommand62Command()
			: base(SubCommand62OperationCode.Unknown)
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			if(Enum.IsDefined(typeof(SubCommand62OperationCode), (byte)OperationCode))
				return $"Unknown Subcommand62 OpCode: {OperationCode:X} Name: {((SubCommand62OperationCode)OperationCode).ToString()} Type: {GetType().Name} CommandSize: {CommandSize * 4 - 2} (bytes size)";
			else
				return $"Unknown Subcommand62 OpCode: {OperationCode:X} Type: {GetType().Name} CommandSize: {CommandSize * 4 - 2} (bytes size)";
		}
	}
}
