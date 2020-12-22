using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// The base type for the subcommand sent in the 0x60 packets.
	/// </summary>
	[DefaultChild(typeof(UnknownSubCommand60Command))]
	[WireDataContract(WireDataContractAttribute.KeyType.Byte, InformationHandlingFlags.DontConsumeRead, true)]
	public abstract class BaseSubCommand60 : ISubCommand60
	{
		/// <summary>
		/// The operation code for the subcommand.
		/// This is only read for logging of unknown subcommands.
		/// </summary>
		[DontWrite]
		[WireMember(1)]
		public SubCommand60OperationCode CommandOperationCode { get; internal set; }

		/// <summary>
		/// Indicates if the <see cref="CommandSize"/> property is serialized and
		/// deserialized.
		/// Child Types can override this to gain access to the single Byte size if needed.
		/// </summary>
		public virtual bool isSizeSerialized { get; } = true;

		//Since the Type byte is eaten by the polymorphic deserialization process
		//We just read the size to discard it
		/// <summary>
		/// The size of the subcommand (subpayload).
		/// Not needed for deserialization of subcommand.
		/// </summary>
		[Optional(nameof(isSizeSerialized))]
		[WireMember(2)]
		public byte CommandSize { get; internal set; }

		//Serializer ctor
		protected BaseSubCommand60()
		{

		}

		/// <inheritdoc />
		public override string ToString()
		{
			if(Enum.IsDefined(typeof(SubCommand60OperationCode), CommandOperationCode))
				return $"Type: {GetType().Name} OpCode: {(SubCommand60OperationCode)CommandOperationCode}:{CommandOperationCode:X} CommandSize: {CommandSize * 4} (byte size)";
			else
				return $"Type: {GetType().Name} OpCode: {CommandOperationCode:X} CommandSize: {CommandSize * 4} (byte size)";
		}
	}
}
