using System;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma
{
	/// <summary>
	/// Similar to <see cref="AttackHitResult"/> but the unknown 2 byte short
	/// comes after the identifier.
	/// </summary>
	[WireDataContract]
	public sealed class TechniqueHitResult
	{
		[WireMember(1)]
		public MapObjectIdentifier ObjectIdentifier { get; internal set; }

		//TODO: What is this?
		[WireMember(2)]
		public short unk1 { get; internal set; }

		/// <inheritdoc />
		public TechniqueHitResult([NotNull] MapObjectIdentifier objectIdentifier)
			: this()
		{
			ObjectIdentifier = objectIdentifier ?? throw new ArgumentNullException(nameof(objectIdentifier));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public TechniqueHitResult()
		{

		}
	}
}
