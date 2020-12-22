using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.BeginZoneTeleporting)]
	public sealed class Sub60ClientZoneTeleportingEventCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		//TODO: What is this?
		[WireMember(2)]
		internal byte unk { get; set; }

		/// <inheritdoc />
		public Sub60ClientZoneTeleportingEventCommand(byte identifier)
		{
			Identifier = identifier;
		}

		public Sub60ClientZoneTeleportingEventCommand()
		{
			
		}
	}
}
