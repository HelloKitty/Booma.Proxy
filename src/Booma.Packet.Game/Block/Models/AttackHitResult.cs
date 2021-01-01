using System;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma
{
	[WireDataContract]
	public sealed class AttackHitResult
	{
		//TODO: What is this?
		[WireMember(1)]
		public short unk1 { get; internal set; }

		[WireMember(2)]
		public MapObjectIdentifier ObjectIdentifier { get; internal set; }

		/// <inheritdoc />
		public AttackHitResult([NotNull] MapObjectIdentifier objectIdentifier)
			: this()
		{
			ObjectIdentifier = objectIdentifier ?? throw new ArgumentNullException(nameof(objectIdentifier));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public AttackHitResult()
		{

		}
	}
}
