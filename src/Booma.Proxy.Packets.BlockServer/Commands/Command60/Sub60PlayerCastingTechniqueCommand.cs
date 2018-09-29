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
	[SubCommand60(SubCommand60OperationCode.CastingTechiqueCast)]
	public sealed class Sub60PlayerCastingTechniqueCommand : BaseSubCommand60, IMessageContextIdentifiable
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
		public bool Hit { get; }

		[Optional(nameof(Hit))]
		[WireMember(5)]
		public MapObjectIdentifier HitIdentifier { get; }

		[Optional(nameof(Hit))]
		[WireMember(6)]
		private short unk2 { get; }

		/// <inheritdoc />
		public Sub60PlayerCastingTechniqueCommand(byte clientId, TechniqueDefinitionData technique, bool hit)
			: this()
		{
			Identifier = clientId;
			Technique = technique;
			Hit = hit;
		}

		//Serializer ctor
		private Sub60PlayerCastingTechniqueCommand()
		{
			//TODO: If this is dynamically sized then change this.
			CommandSize = (byte)(Hit ? ((8 + 4) / 4) : (8 / 4));
		}
	}
}
