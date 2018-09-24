using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	public sealed class CharacterCustomizationInfo
	{
		/// <summary>
		/// Id for the character's costume.
		/// </summary>
		[WireMember(1)]
		public ushort CostumeId { get; }

		/// <summary>
		/// Id for the character's skin.
		/// </summary>
		[WireMember(2)]
		public ushort SkinId { get; }

		/// <summary>
		/// Id for the character's face.
		/// </summary>
		[WireMember(3)]
		public ushort FaceId { get; }

		/// <summary>
		/// Id for the character's head.
		/// </summary>
		[WireMember(4)]
		public ushort HeadId { get; }
		
		/// <summary>
		/// Id for the character's hair.
		/// </summary>
		[WireMember(5)]
		public ushort HairId { get; }

		/// <summary>
		/// The hair color.
		/// </summary>
		[WireMember(6)]
		public Vector3<ushort> HairColor { get; }

		/// <summary>
		/// The proportions foe the character.
		/// </summary>
		[WireMember(7)]
		public Vector2<float> Proportions { get; }

		//Serializer ctor
		private CharacterCustomizationInfo()
		{
			
		}
	}
}