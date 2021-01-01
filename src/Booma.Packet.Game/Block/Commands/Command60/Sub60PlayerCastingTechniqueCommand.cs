using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	//https://sylverant.net/wiki/index.php/Packet_0x60#Subcommand_0x47
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.CastingTechiqueCast)]
	public sealed partial class Sub60PlayerCastingTechniqueCommand : BaseSubCommand60, IMessageContextIdentifiable
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

		/// <summary>
		/// Indicates if the spell cast has any targets.
		/// </summary>
		public bool HasTargets => HitIdentifiers != null && HitIdentifiers.Length != 0;

		[SendSize(PrimitiveSizeType.Byte)]
		[WireMember(5)]
		internal TechniqueHitResult[] HitIdentifiers { get; set; } = new TechniqueHitResult[0];

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

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60PlayerCastingTechniqueCommand()
			: base(SubCommand60OperationCode.CastingTechiqueCast)
		{
			//TODO: If this is dynamically sized then change this.
			CommandSize = (byte)(HasTargets ? ((8 + 4 * HitIdentifiers.Length) / 4) : (8 / 4));
		}
	}
}
