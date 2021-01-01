using System;
using System.ComponentModel;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Represents the information about a character for special configuration
	/// such as name coloring or NPC skins.
	/// </summary>
	[WireDataContract]
	public sealed class CharacterSpecialCustomInfo
	{
		//TODO: Why is this a uint?
		/// <summary>
		/// The color of the name.
		/// </summary>
		[WireMember(1)]
		public uint NameColor { get; internal set; }

		/// <summary>
		/// Model type.
		/// (Ex. Regular, Rico, Sonic, Tails)
		/// </summary>
		[EnumSize(PrimitiveSizeType.Byte)]
		[WireMember(2)]
		public CharacterModelType ModelType { get; internal set; }

		//TODO: For lobby character data Sylverant sends playtime here?
		//Claims: /* Placed here, like newserv */
		//TODO: Why? What?
		[KnownSize(15)]
		[WireMember(3)]
		internal byte[] unused { get; set; } = Array.Empty<byte>();

		//TODO: How does this work?
		/// <summary>
		/// The checksum for the name color.
		/// </summary>
		[WireMember(4)]
		public uint ColoredNameChecksum { get; internal set; }

		public CharacterSpecialCustomInfo(uint nameColor, 
			CharacterModelType modelType, 
			uint coloredNameChecksum)
		{
			if(!Enum.IsDefined(typeof(CharacterModelType), modelType)) throw new InvalidEnumArgumentException(nameof(modelType), (int)modelType, typeof(CharacterModelType));

			NameColor = nameColor;
			ModelType = modelType;
			ColoredNameChecksum = coloredNameChecksum;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterSpecialCustomInfo()
		{
			
		}
	}
}
