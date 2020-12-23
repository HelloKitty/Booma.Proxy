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
		public byte Identifier { get; internal set; }

		//Always a byte after identifier.
		[WireMember(2)]
		internal byte unk1 { get; set; }

		//Unknown 4 bytes at the end of this command.
		[WireMember(3)]
		internal int unk2 { get; set; }

		/// <inheritdoc />
		public Sub60PlayerBeginTechniqueCastCommand(byte identifier)
			: this()
		{
			Identifier = identifier;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60PlayerBeginTechniqueCastCommand()
			: base(SubCommand60OperationCode.BeginTechniqueCast)
		{
			CommandSize = 8 / 4;
		}
	}
}
