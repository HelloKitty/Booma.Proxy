using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Damage flag1 contains status effects data.
	/// </summary>
	[Flags]
	public enum DamageFlag1 : byte
	{
		Unknown0 = 0,
		Paralysis = 0b0000_0010,
		Confusion = 0b0001_0000,
		Frozen = 0b0010_0000

		//0100 (4) is nothing?
		//1000 (8) is nothing too
		//All other bits don't seem to do anything, at least on normal creatures.
	}

	[Flags]
	public enum DamageFlag2 : byte
	{
		//Maybe? I can't tell if it's special effect.
		SpecialEffect = 0b0000_0000,

		//This 1 bit is sent when you use stuff like Dim Mechguns AND they miss. Last 4: 00 01 90 20
		//TODO: I don't think this is multiple hit, maybe has something to do with accuracy. Randomly pops up sometimes, don't know why. Maybe a miss?
		Unknown1 = 0b0000_0001, //this one alone will cause no visual, this one alone is sometimes sent along with 3 packets when something gets confused
		Hit = 0b0000_0010,
		HeavyHit = 0b0000_0100,
		CausedDeath = 0b0000_1000,

		//Seems to make the creature forget anyone is in the room, will reset and ignore players.
		Blind = 0b0001_0000
	}

	//Seems to go from 0x90 to 0xB0 if it interupts an attack animation?
	[Flags]
	public enum DamageFlag3 : byte
	{
		Unknown9 = 9,
		Unknown11 = 11,

		//Probably unused, or a glitch, but it freezes the creature into an idle state.
		FreezeForever = 0b0100_0000,
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
		public MapObjectIdentifier ObjectIdentifier { get; internal set; }

		//Why is this sent twice? Maybe when creatures deal damage to other creatures??
		//This version is missing the objecttype/floor
		[WireMember(2)]
		internal short ObjectIdentifier2 { get; set; }

		/// <summary>
		/// Usually the new health deficiet of the object/creature.
		/// </summary>
		[WireMember(3)]
		internal ushort TotalDamageTaken { get; set; }

		/// <summary>
		/// Unknown, seems to always be 0.
		/// </summary>
		[WireMember(4)]
		internal byte UnknownFlag1 { get; set; }

		[WireMember(5)]
		internal DamageFlag2 UnknownFlag2 { get; set; }

		[WireMember(6)]
		internal DamageFlag3 UnknownFlag3 { get; set; }

		[WireMember(7)]
		internal byte UnknownFlag4 { get; set; }

		/// <inheritdoc />
		public Sub60ObjectDamageRecievedCommand(MapObjectIdentifier objectIdentifier, ushort totalDamageTaken, byte[] flags)
			: this()
		{
			ObjectIdentifier = objectIdentifier;
			ObjectIdentifier2 = objectIdentifier.Identifier; //TODO: We are actually sending extra things we shouldn't here, we may need to change it
			TotalDamageTaken = totalDamageTaken;

			//TODO: Legacy reason, replace this with flag enums in cto
			UnknownFlag1 = flags[0];
			UnknownFlag2 = (DamageFlag2)flags[1];
			UnknownFlag3 = (DamageFlag3)flags[2];
			UnknownFlag4 = flags[3];
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60ObjectDamageRecievedCommand()
			: base(SubCommand60OperationCode.ObjectDamageHit)
		{
			CommandSize = 0x0C / 4;
		}
	}
}
