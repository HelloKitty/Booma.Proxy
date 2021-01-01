using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	//Based on Syl: https://github.com/Sylverant/libsylverant/blob/e1a01d5586ed12d41b99c5cf1ba955e32b173950/include/sylverant/characters.h#L126
	/// <summary>
	/// Character data model.
	/// </summary>
	[WireDataContract]
	public sealed class PlayerCharacterDataModel
	{
		/// <summary>
		/// The progress for the character.
		/// </summary>
		[WireMember(1)]
		public CharacterProgress Progress { get; internal set; }

		//TODO: Is this just the guild card as a string?
		/// <summary>
		/// The guild card.
		/// </summary>
		[DontTerminate]
		[KnownSize(16)]
		[WireMember(2)]
		public string GuildCard { get; internal set; }

		[WireMember(3)]
		internal ulong unk3 { get; set; }

		/// <summary>
		/// The special character data such as name color
		/// or NPC skin.
		/// </summary>
		[WireMember(4)]
		public CharacterSpecialCustomInfo Special { get; internal set; }

		/// <summary>
		/// Section ID of the character.
		/// </summary>
		[WireMember(5)]
		public SectionId SectionId { get; internal set; }

		/// <summary>
		/// The class/race for the character.
		/// </summary>
		[WireMember(6)]
		public CharacterClass ClassRace { get; internal set; }

		//TODO: What is this?
		/// <summary>
		/// Character version data.
		/// </summary>
		[WireMember(7)]
		public CharacterVersionData VersionData { get; internal set; }

		/// <summary>
		/// Character customization information/data
		/// (Ex. Hair, Costume)
		/// </summary>
		[WireMember(8)]
		public CharacterCustomizationInfo CustomizationInfo { get; internal set; }

		/// <summary>
		/// The name of the character.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[DontTerminate] //don't terminate, knownsize sends all zero bytes up to the length. (so 15 char)
		[KnownSize(16)] //TODO: Destiny only sends 15 char for char name. Other servers use 16.
		[WireMember(9)]
		public string CharacterName { get; internal set; }

		/// <summary>
		/// The amount of time the character has played.
		/// </summary>
		[WireMember(10)]
		public uint PlayedTime { get; internal set; }

		public PlayerCharacterDataModel(CharacterProgress progress, string guildCard, CharacterSpecialCustomInfo special, 
			SectionId sectionId, CharacterClass classRace, 
			CharacterVersionData versionData, CharacterCustomizationInfo customizationInfo, 
			string characterName, uint playedTime)
		{
			if (!Enum.IsDefined(typeof(SectionId), sectionId)) throw new InvalidEnumArgumentException(nameof(sectionId), (int) sectionId, typeof(SectionId));
			if (!Enum.IsDefined(typeof(CharacterClass), classRace)) throw new InvalidEnumArgumentException(nameof(classRace), (int) classRace, typeof(CharacterClass));

			Progress = progress ?? throw new ArgumentNullException(nameof(progress));
			GuildCard = guildCard ?? throw new ArgumentNullException(nameof(guildCard));

			//TODO: What is this?
			this.unk3 = 0;

			Special = special;
			SectionId = sectionId;
			ClassRace = classRace;
			VersionData = versionData ?? throw new ArgumentNullException(nameof(versionData));
			CustomizationInfo = customizationInfo ?? throw new ArgumentNullException(nameof(customizationInfo));
			CharacterName = characterName ?? throw new ArgumentNullException(nameof(characterName));
			PlayedTime = playedTime;
		}

		//Serializer ctor
		public PlayerCharacterDataModel()
		{
			
		}
	}
}
