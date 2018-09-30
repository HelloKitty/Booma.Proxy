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

		/// <summary>
		/// Indicates if the spell cast has any targets.
		/// </summary>
		public bool HasTargets => HitIdentifiers != null && HitIdentifiers.Length != 0;

		[SendSize(SendSizeAttribute.SizeType.Byte)]
		[WireMember(5)]
		private TechniqueHitResult[] HitIdentifiers { get; } = new TechniqueHitResult[0];

		/// <summary>
		/// Creates a new technique cast with no targets.
		/// </summary>
		/// <param name="clientId"></param>
		/// <param name="technique"></param>
		public Sub60PlayerCastingTechniqueCommand(byte clientId, TechniqueDefinitionData technique)
			: this()
		{
			Identifier = clientId;
			Technique = technique;
		}

		/// <summary>
		/// Creates a new technique cast with no targets.
		/// </summary>
		/// <param name="clientId"></param>
		/// <param name="technique"></param>
		public Sub60PlayerCastingTechniqueCommand(byte clientId, TechniqueDefinitionData technique, params TechniqueHitResult[] hits)
			: this(clientId, technique)
		{
			HitIdentifiers = hits;
		}

		//Serializer ctor
		private Sub60PlayerCastingTechniqueCommand()
		{
			//TODO: If this is dynamically sized then change this.
			CommandSize = (byte)(HasTargets ? ((8 + 4 * HitIdentifiers.Length) / 4) : (8 / 4));
		}
	}
}
