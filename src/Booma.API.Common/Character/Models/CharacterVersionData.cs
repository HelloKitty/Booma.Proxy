using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Currently unknown version data for the character.
	/// </summary>
	[WireDataContract]
	public sealed class CharacterVersionData
	{
		//TODO: What is this?
		[WireMember(1)]
		internal byte V2Flags { get; set; }

		//TODO: What is this?
		[WireMember(2)]
		internal byte Version { get; set; }

		//TODO: What is this?
		[WireMember(3)]
		internal uint V1Flags { get; set; }

		public CharacterVersionData(byte v2Flags, byte version, uint v1Flags)
		{
			V2Flags = v2Flags;
			Version = version;
			V1Flags = v1Flags;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterVersionData()
		{
			
		}
	}
}
