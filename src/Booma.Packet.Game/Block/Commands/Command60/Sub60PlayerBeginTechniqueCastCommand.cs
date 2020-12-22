using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// 0x60 0x8D BeginTechniqueCast.
	/// </summary>
	//https://sylverant.net/wiki/index.php/Packet_0x60#Subcommand_0x8D
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.BeginTechniqueCast)]
	public sealed class Sub60PlayerBeginTechniqueCastCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; }

		//Always a byte after identifier.
		[WireMember(2)]
		private byte unk1 { get; }

		//Unknown 4 bytes at the end of this command.
		[WireMember(3)]
		private int unk2 { get; }

		/// <inheritdoc />
		public Sub60PlayerBeginTechniqueCastCommand(byte identifier)
			: this()
		{
			Identifier = identifier;
		}

		//Serializer ctor
		private Sub60PlayerBeginTechniqueCastCommand()
		{
			CommandSize = 8 / 4;
		}
	}
}
