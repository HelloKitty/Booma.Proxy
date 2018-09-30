using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//https://sylverant.net/wiki/index.php/Packet_0x60#Subcommand_0x76
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.CreatureDeathEvent)]
	public sealed class Sub60CreatureDeathEventCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		public byte Identifier => ObjectIdentifier.Identifier;

		[WireMember(1)]
		public MapObjectIdentifier ObjectIdentifier { get; }

		//Unknown, is 01 00 08 00 sometimes.
		[WireMember(2)]
		public int Unk1 { get; }

		//Serializer ctor
		private Sub60CreatureDeathEventCommand()
		{
			CommandSize = 12 / 4;
		}
	}
}
