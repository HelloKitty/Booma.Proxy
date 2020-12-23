using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//https://sylverant.net/wiki/index.php/Packet_0x60#Subcommand_0x47
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.FinishedTechniqueCast)]
	public sealed partial class Sub60PlayFinishedTechniqueCastCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		[WireMember(2)]
		internal byte unk1 { get; set; }

		/// <summary>
		/// The technique being cast.
		/// </summary>
		[WireMember(3)]
		public TechniqueDefinitionData Technique { get; internal set; }

		//TODO: might be lenth prefix size for hit result
		[WireMember(4)]
		public byte Unk3 { get; internal set; }

		/// <inheritdoc />
		public Sub60PlayFinishedTechniqueCastCommand(byte clientId, TechniqueDefinitionData technique)
			: this()
		{
			Identifier = clientId;
			Technique = technique;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60PlayFinishedTechniqueCastCommand()
			: base(SubCommand60OperationCode.FinishedTechniqueCast)
		{
			//TODO: If this is dynamically sized then change this.
			CommandSize = 8 / 4;
		}
	}
}
