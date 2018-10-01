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
		private short ObjectIdentifier2 { get; }

		/// <summary>
		/// Usually the new health deficiet of the object/creature.
		/// </summary>
		[WireMember(3)]
		private ushort TotalDamageTaken { get; }

		/// <summary>
		/// Unknown flags.
		/// </summary>
		[WireMember(4)]
		private uint Flags { get; }

		/// <inheritdoc />
		public Sub60ObjectDamageRecievedCommand(MapObjectIdentifier objectIdentifier, ushort totalDamageTaken, uint flags)
			: this()
		{
			ObjectIdentifier = objectIdentifier;
			ObjectIdentifier2 = objectIdentifier.Identifier; //TODO: We are actually sending extra things we shouldn't here, we may need to change it
			TotalDamageTaken = totalDamageTaken;
			Flags = flags;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private Sub60ObjectDamageRecievedCommand()
		{
			CommandSize = 0x0C / 4;
		}
	}
}
