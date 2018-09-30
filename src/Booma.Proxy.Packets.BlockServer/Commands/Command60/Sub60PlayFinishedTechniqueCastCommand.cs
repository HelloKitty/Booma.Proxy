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
	public sealed class Sub60PlayFinishedTechniqueCastCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; }

		[WireMember(2)]
		private byte unk1 { get; }

		/// <summary>
		/// The technique being cast.
		/// </summary>
		[WireMember(3)]
		public TechniqueDefinitionData Technique { get; }

		//TODO: might be lenth prefix size for hit result
		[WireMember(4)]
		public byte Unk3 { get; }

		/// <inheritdoc />
		public Sub60PlayFinishedTechniqueCastCommand(byte clientId, TechniqueDefinitionData technique)
			: this()
		{
			Identifier = clientId;
			Technique = technique;
		}

		//Serializer ctor
		private Sub60PlayFinishedTechniqueCastCommand()
		{
			//TODO: If this is dynamically sized then change this.
			CommandSize = 8 / 4;
		}
	}
}
