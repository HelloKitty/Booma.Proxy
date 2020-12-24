using System;
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
		public ushort CostumeId { get; internal set; }

		/// <summary>
		/// Id for the character's skin.
		/// </summary>
		[WireMember(2)]
		public ushort SkinId { get; internal set; }

		/// <summary>
		/// Id for the character's face.
		/// </summary>
		[WireMember(3)]
		public ushort FaceId { get; internal set; }

		/// <summary>
		/// Id for the character's head.
		/// </summary>
		[WireMember(4)]
		public ushort HeadId { get; internal set; }
		
		/// <summary>
		/// Id for the character's hair.
		/// </summary>
		[WireMember(5)]
		public ushort HairId { get; internal set; }

		/// <summary>
		/// The hair color.
		/// </summary>
		[WireMember(6)]
		public Vector3<ushort> HairColor { get; internal set; }

		/// <summary>
		/// The proportions foe the character.
		/// </summary>
		[WireMember(7)]
		public Vector2<float> Proportions { get; internal set; }

		public CharacterCustomizationInfo(ushort costumeId, 
			ushort skinId, 
			ushort faceId, 
			ushort headId, 
			ushort hairId, 
			Vector3<ushort> hairColor, 
			Vector2<float> proportions)
		{
			CostumeId = costumeId;
			SkinId = skinId;
			FaceId = faceId;
			HeadId = headId;
			HairId = hairId;
			HairColor = hairColor ?? throw new ArgumentNullException(nameof(hairColor));
			Proportions = proportions ?? throw new ArgumentNullException(nameof(proportions));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterCustomizationInfo()
		{
			
		}
	}
}
