using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[Flags]
	public enum DamageFlags2
	{
		MultipleHit = 0b0000_0001,
		NormalHit = 0b0000_0010,
		HeavyHit = 0b0000_0100,
		CausedDeath = 0b0000_1000
	}

	[Flags]
	public enum DamageFlag3 : byte
	{
		Unknown9 = 9,
		Unknown11 = 11,
	}

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
		/// Unknown, seems to always be 0.
		/// </summary>
		[WireMember(4)]
		private byte UnknownFlag1 { get; }

		[WireMember(5)]
		private DamageFlag2 UnknownFlag2 { get; }

		[WireMember(6)]
		private DamageFlag3 UnknownFlag3 { get; }

		[WireMember(7)]
		private byte UnknownFlag4 { get; }

		/// <inheritdoc />
		public Sub60ObjectDamageRecievedCommand(MapObjectIdentifier objectIdentifier, ushort totalDamageTaken, byte[] flags)
			: this()
		{
			ObjectIdentifier = objectIdentifier;
			ObjectIdentifier2 = objectIdentifier.Identifier; //TODO: We are actually sending extra things we shouldn't here, we may need to change it
			TotalDamageTaken = totalDamageTaken;

			//TODO: Legacy reason
			UnknownFlag1 = flags[0];
			UnknownFlag2 = (DamageFlag2)flags[1];
			UnknownFlag3 = (DamageFlag3)flags[2];
			UnknownFlag4 = flags[3];
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
