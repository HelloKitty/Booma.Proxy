using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//https://sylverant.net/wiki/index.php/Packet_0x60#Subcommand_0x0A
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.ObjectDamageHit)]
	public sealed class Sub60ObjectDamageRecievedCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		public byte Identifier => ObjectIdentifier.Identifier;

		//Most likely a creature, but could be other things.
		[WireMember(1)]
		public MapObjectIdentifier ObjectIdentifier { get; }

		//Why is this sent twice? Maybe when creatures deal damage to other creatures??
		//This version is missing the objecttype/floor
		[WireMember(2)]
		private MapObjectIdentifier ObjectIdentifier2 { get; }

		/// <summary>
		/// Usually the new HP of the object
		/// but in rare instances it is the amount of damage dealt to a creature.
		/// (Ex. Tofu says for De Rol this is damage dealt)
		/// </summary>
		[WireMember(3)]
		private ushort ObjectDamageUpdate { get; }

		/// <summary>
		/// Unknown flags.
		/// </summary>
		[WireMember(4)]
		private uint Flags { get; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private Sub60ObjectDamageRecievedCommand()
		{
			CommandSize = 0x0C / 4;
		}
	}
}
