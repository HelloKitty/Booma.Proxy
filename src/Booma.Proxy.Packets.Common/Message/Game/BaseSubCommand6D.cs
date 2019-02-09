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
	[WireDataContract(WireDataContractAttribute.KeyType.Byte, InformationHandlingFlags.DontConsumeRead, true)]
	public abstract class BaseSubCommand6D : ISubCommand6D
	{
		/// <summary>
		/// The operation code for the subcommand.
		/// This is only read for logging of unknown subcommands.
		/// </summary>
		[DontWrite]
		[WireMember(1)]
		public SubCommand6DOperationCode CommandOperationCode { get; }

		//Usually this is where the CommandSize would go based on ints
		//but it's a 0 byte here
		[WireMember(2)]
		private byte HeaderUnk1 { get; set; }
		
		[WireMember(3)]
		private short HeaderUnk2 { get; set; }

		//Since the Type byte is eaten by the polymorphic deserialization process
		//We just read the size to discard it
		/// <summary>
		/// The size of the subcommand (subpayload).
		/// </summary>
		[WireMember(4)]
		public int CommandSize { get; protected set; }

		//Serializer ctor
		protected BaseSubCommand6D()
		{

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
