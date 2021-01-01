using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// The base type for the subcommand sent in the 0x60 packets.
	/// </summary>
	[WireMessageType]
	[DefaultChild(typeof(UnknownSubCommand60Command))]
	[WireDataContract(PrimitiveSizeType.Byte)]
	public abstract partial class BaseSubCommand60 : ISubCommand60
	{
		//TODO: Technicallt only 1 byte sometimes, as size is optionally serialized.
		/// <summary>
		/// Binary size of the <see cref="BaseSubCommand60"/> header.
		/// </summary>
		public const int COMMAND_HEADER_SIZE = sizeof(byte) + sizeof(byte);

		/// <summary>
		/// The operation code for the subcommand.
		/// This is only read for logging of unknown subcommands.
		/// </summary>
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
		public byte CommandSize { get; protected internal set; }

		//Serializer ctor
		protected BaseSubCommand60(SubCommand60OperationCode commandOperationCode)
		{
			//This is in a serialization hotpath so we don't verify the enum with
			//and throw because it depends on slow reflection.
			CommandOperationCode = commandOperationCode;
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
