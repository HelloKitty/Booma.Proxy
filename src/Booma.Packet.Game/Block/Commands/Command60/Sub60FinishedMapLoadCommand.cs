using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload sent by the client when it has finished loading the map.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.SUBCMD_LOAD_3B)]
	public sealed class Sub60FinishedMapLoadCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		//Empty command that just notifies other players of finished loading map

		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; internal set; }
		
		//TODO: What is this? Could be the map we loaded?
		[WireMember(2)]
		internal byte unk { get; set; }

		public Sub60FinishedMapLoadCommand(byte identifier)
			: this()
		{
			Identifier = identifier;
		}

		public Sub60FinishedMapLoadCommand(int identifier)
			: this((byte)identifier)
		{

		}

		//Serialzier ctor
		private Sub60FinishedMapLoadCommand()
		{
			CommandSize = 4 / 4;
		}
	}
}
