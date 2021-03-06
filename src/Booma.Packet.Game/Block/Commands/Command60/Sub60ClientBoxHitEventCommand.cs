using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	//https://sylverant.net/wiki/index.php/Packet_0x60#Subcommand_0x0B
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.GameBoxHit)]
	public sealed partial class Sub60ClientBoxHitEventCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		public byte Identifier => ObjectIdentifier.Identifier;

		[WireMember(1)]
		public MapObjectIdentifier ObjectIdentifier { get; internal set; }

		//TODO: What is this?
		//Sylverant says this is always 1? Also considers the first and subsequent 3 bytes sepertae but we'll leave this
		//together for now
		/// <summary>
		/// TODO: ?
		/// </summary>
		[WireMember(2)]
		internal int unk1 { get; set; } = 1;

		//Slyverant says the last 3 bytes of this are always 0. So, we'll guess it's a 4 byte value instead. Little enditan
		//TODO: What is this?
		/// <summary>
		/// TODO: ?
		/// </summary>
		[WireMember(3)]
		internal short Identifier2_unk2 { get; set; }

		[WireMember(4)]
		internal short unk3 { get; set; } = 0;

		/// <inheritdoc />
		public Sub60ClientBoxHitEventCommand(MapObjectIdentifier objectIdentifier)
			: this()
		{
			ObjectIdentifier = objectIdentifier;
			Identifier2_unk2 = objectIdentifier.ObjectIndex; //don't include object type for some reason
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60ClientBoxHitEventCommand()
			: base(SubCommand60OperationCode.GameBoxHit)
		{
			CommandSize = 12 / 4;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"Id: {Identifier} Unk1: {unk1} Id2unk: {Identifier2_unk2}";
		}
	}
}
