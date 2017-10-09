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
		public uint NameColor { get; }

		/// <summary>
		/// Model type.
		/// (Ex. Regular, Rico, Sonic, Tails)
		/// </summary>
		[WireMember(2)]
		public CharacterModelType ModelType { get; }

		[KnownSize(15)]
		[WireMember(3)]
		private byte[] unused { get; } = new byte[15];

		//TODO: How does this work?
		/// <summary>
		/// The checksum for the name color.
		/// </summary>
		[KnownSize(4)]
		[WireMember(4)]
		public byte[] ColoredNameChecksum { get; }

		//Serializer ctor
		private CharacterSpecialCustomInfo()
		{
			
		}
	}
}