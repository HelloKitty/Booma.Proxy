using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//https://sylverant.net/wiki/index.php/Packet_0x60#Subcommand_0x0B
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.GameBoxHit)]
	public sealed class Sub60ClientBoxHitEventCommand : BaseSubCommand60
	{
		//Teth and Soly's server both remove the most significant 4 bits
		//when computing the ID.
		[WireMember(1)]
		private short _BoxId { get; }

		/// <inheritdoc />
		public short BoxId => (short)(_BoxId & 0x0FFF);

		public int SignificantBitsId => _BoxId & 0xF000;

		//TODO: What is this?
		//Sylverant says this is always 1? Also considers the first and subsequent 3 bytes sepertae but we'll leave this
		//together for now
		/// <summary>
		/// TODO: ?
		/// </summary>
		[WireMember(2)]
		private int unk1 { get; } = 1;

		//Slyverant says the last 3 bytes of this are always 0. So, we'll guess it's a 4 byte value instead. Little enditan
		//TODO: What is this?
		/// <summary>
		/// TODO: ?
		/// </summary>
		[WireMember(3)]
		public short Identifier2_unk2 { get; }

		//Serializer ctor
		private Sub60ClientBoxHitEventCommand()
		{
			CommandSize = 12 / 4;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"Id: {BoxId} Unk1: {unk1} Id2unk: {Identifier2_unk2} SigBits: {SignificantBitsId}";
		}
	}
}
