using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// The model for progress of a character.
	/// </summary>
	[WireDataContract]
	public sealed class CharacterProgress
	{
		/// <summary>
		/// Experience earned by the character.
		/// </summary>
		[WireMember(1)]
		public uint Experience { get; internal set; }

		/// <summary>
		/// Level of the character.
		/// </summary>
		[WireMember(2)]
		public uint Level { get; internal set; }

		public CharacterProgress(uint experience, uint level)
		{
			Experience = experience;
			Level = level;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterProgress()
		{
			
		}
	}
}
