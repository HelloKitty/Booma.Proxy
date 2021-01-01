using FreecraftCore.Serializer;

namespace Booma
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
		/// (0 is Level 1.)
		/// </summary>
		[WireMember(2)]
		public uint RawLevel { get; internal set; }

		/// <summary>
		/// Represents the real actual level of the player.
		/// This value is never zero.
		/// </summary>
		public int RealLevel => (int) (RawLevel + 1);

		public CharacterProgress(uint experience, uint rawLevel)
		{
			Experience = experience;
			RawLevel = rawLevel;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterProgress()
		{
			
		}
	}
}
