using FreecraftCore.Serializer;

namespace Booma.Proxy
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
		[WireMember(2)]
		public CharacterModelType ModelType { get; internal set; }

		//TODO: Why? What?
		[KnownSize(15)]
		[WireMember(3)]
		internal byte[] unused { get; set; } = new byte[15];

		//TODO: How does this work?
		/// <summary>
		/// The checksum for the name color.
		/// </summary>
		[WireMember(4)]
		public uint ColoredNameChecksum { get; internal set; }

		//Serializer ctor
		private CharacterSpecialCustomInfo()
		{
			
		}
	}
}
