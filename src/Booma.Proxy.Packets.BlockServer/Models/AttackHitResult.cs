using System;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	[WireDataContract]
	public sealed class AttackHitResult
	{
		//TODO: What is this?
		[WireMember(1)]
		public short unk1 { get; }

		[WireMember(2)]
		public MapObjectIdentifier ObjectIdentifier { get; }

		/// <inheritdoc />
		public AttackHitResult([NotNull] MapObjectIdentifier objectIdentifier)
		{
			ObjectIdentifier = objectIdentifier ?? throw new ArgumentNullException(nameof(objectIdentifier));
		}

		private AttackHitResult()
		{

		}
	}
}