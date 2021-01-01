using System;
using System.Collections.Generic;
using System.Text;
using Booma.Proxy;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Character data for the lobby. Similar to <see cref="PlayerCharacterDataModel"/> but
	/// with some slightly different structure.
	/// </summary>
	[WireDataContract]
	public sealed class LobbyCharacterData
	{
		[WireMember(1)]
		public CharacterStats Stats { get; internal set; }

		[WireMember(2)]
		internal ushort unk1 { get; set; }

		[WireMember(3)]
		internal ulong unk2 { get; set; }

		/// <summary>
		/// The progress for the character.
		/// </summary>
		[WireMember(4)]
		public CharacterProgress Progress { get; internal set; }

		/// <summary>
		/// The Meseta/Money of the character.
		/// </summary>
		[WireMember(5)]
		public uint Money { get; internal set; }

		//TODO: Is this just the guild card as a string?
		/// <summary>
		/// The guild card.
		/// </summary>
		[DontTerminate]
		[KnownSize(16)]
		[WireMember(6)]
		public string GuildCard { get; internal set; }

		[WireMember(7)]
		internal ulong unk3 { get; set; }

		/// <summary>
		/// The special character data such as name color
		/// or NPC skin.
		/// </summary>
		[WireMember(8)]
		public CharacterSpecialCustomInfo Special { get; internal set; }

		/// <summary>
		/// Section ID of the character.
		/// </summary>
		[WireMember(9)]
		public SectionId SectionId { get; internal set; }

		/// <summary>
		/// The class/race for the character.
		/// </summary>
		[WireMember(10)]
		public CharacterClass ClassRace { get; internal set; }

		//TODO: What is this?
		/// <summary>
		/// Character version data.
		/// </summary>
		[WireMember(11)]
		public CharacterVersionData VersionData { get; internal set; }

		/// <summary>
		/// Character customization information/data
		/// (Ex. Hair, Costume)
		/// </summary>
		[WireMember(12)]
		public CharacterCustomizationInfo CustomizationInfo { get; internal set; }

		/// <summary>
		/// The name of the character.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[DontTerminate] //don't terminate, knownsize sends all zero bytes up to the length. (so 15 char)
		[KnownSize(16)] //TODO: Destiny only sends 15 char for char name. Other servers use 16.
		[WireMember(13)]
		public string CharacterName { get; internal set; }

		//This is pallete/actionbar according to Soly.
		//TODO: Research what this is.
		[KnownSize(0xE8)]
		[WireMember(14)]
		public byte[] ActionBarSettings { get; internal set; } = Array.Empty<byte>();

		[KnownSize(0x14)]
		[WireMember(15)]
		public byte[] Techniques { get; set; } = Array.Empty<byte>();

		public LobbyCharacterData(CharacterStats stats, ushort unk1, ulong unk2, CharacterProgress progress, uint money, string guildCard, ulong unk3, CharacterSpecialCustomInfo special, SectionId sectionId, CharacterClass classRace, CharacterVersionData versionData, CharacterCustomizationInfo customizationInfo, string characterName)
		{
			Stats = stats ?? throw new ArgumentNullException(nameof(stats));
			this.unk1 = unk1;
			this.unk2 = unk2;
			Progress = progress ?? throw new ArgumentNullException(nameof(progress));
			Money = money;
			GuildCard = guildCard ?? throw new ArgumentNullException(nameof(guildCard));
			this.unk3 = unk3;
			Special = special ?? throw new ArgumentNullException(nameof(special));
			SectionId = sectionId;
			ClassRace = classRace;
			VersionData = versionData ?? throw new ArgumentNullException(nameof(versionData));
			CustomizationInfo = customizationInfo ?? throw new ArgumentNullException(nameof(customizationInfo));
			CharacterName = characterName ?? throw new ArgumentNullException(nameof(characterName));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public LobbyCharacterData()
		{
			
		}
	}
}
