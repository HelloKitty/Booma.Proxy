using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//6D also sends the target play in the Flags
	//Example: 6D 00 02 00 00 00 targets player index 02.
	/// <summary>
	/// The base type for the subcommand sent in the 0x6D packets.
	/// </summary>
	[DefaultChild(typeof(UnknownSubCommand6DCommand))]
	[WireDataContract(PrimitiveSizeType.Byte)]
	public abstract partial class BaseSubCommand6D : ISubCommand6D
	{
		/// <summary>
		/// The operation code for the subcommand.
		/// This is only read for logging of unknown subcommands.
		/// </summary>
		[WireMember(1)]
		public SubCommand6DOperationCode CommandOperationCode { get; internal set; }

		//Usually this is where the CommandSize would go based on ints
		//but it's a 0 byte here
		[WireMember(2)]
		internal byte HeaderUnk1 { get; set; }
		
		/// <summary>
		/// The sender index/identifier that the packet is for.
		/// </summary>
		[WireMember(3)]
		public byte OptionalIdentifier { get; internal set; }

		[WireMember(4)]
		internal byte HeaderUnk3 { get; set; }

		//Since the Type byte is eaten by the polymorphic deserialization process
		//We just read the size to discard it
		/// <summary>
		/// The size of the subcommand (subpayload).
		/// </summary>
		[WireMember(5)]
		public int CommandSize { get; protected internal set; }

		/// <summary>
		/// One of the sub6D commands actually sends the sender
		/// in the above byte.
		/// </summary>
		/// <param name="commandOperationCode"></param>
		/// <param name="optionalIdentifier"></param>
		protected BaseSubCommand6D(SubCommand6DOperationCode commandOperationCode, byte optionalIdentifier)
			: this(commandOperationCode)
		{
			OptionalIdentifier = optionalIdentifier;
		}

		protected BaseSubCommand6D(SubCommand6DOperationCode commandOperationCode)
		{
			//This is in a serialization hotpath so we don't verify the enum with
			//and throw because it depends on slow reflection.
			CommandOperationCode = commandOperationCode;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			if(Enum.IsDefined(typeof(SubCommand6DOperationCode), CommandOperationCode))
				return $"Type: {GetType().Name} OpCode: {(SubCommand6DOperationCode)CommandOperationCode}:{CommandOperationCode:X} CommandSize: {CommandSize} (byte size)";
			else
				return $"Type: {GetType().Name} OpCode: {CommandOperationCode:X} CommandSize: {CommandSize} (byte size)";
		}
	}
}
