using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//Do not confuse this with the character data at the character screen called "mini" in Sylverant.
	//Based on: https://github.com/Sylverant/libsylverant/blob/7f7e31d90da1b02c8d89d055628540ee3ad59417/include/sylverant/characters.h#L123
	[WireDataContract]
	public sealed class LobbyCharacterData
	{
		[WireMember(1)]
		public CharacterStats Stats { get; internal set; }

		/*uint16_t unk1;
		uint32_t unk2[2];*/

		[WireMember(2)]
		internal ushort unk1 { get; set; }

		[WireMember(3)]
		internal ulong unk2 { get; set; }

		[WireMember(4)]
		public CharacterProgress Progress { get; internal set; }

		[WireMember(5)]
		public int Meseta { get; internal set; }

		//TODO: Is this just the guild card as a string?
		/// <summary>
		/// The guild card.
		/// </summary>
		[Encoding(EncodingType.ASCII)]
		[DontTerminate]
		[KnownSize(16)]
		[WireMember(6)]
		public string GuildCard { get; internal set; }

		//TODO: What is this?
		[KnownSize(2)]
		[WireMember(7)]
		internal uint[] unk3 { get; set; }

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
		[DontTerminate]
		[KnownSize(16)] //TODO: Destiny only sends 15 char for char name. Other servers use 16.
		[WireMember(13)]
		public string CharacterName { get; internal set; }

		//This is pallete/actionbar according to Soly.
		[KnownSize(0xE8)]
		[WireMember(14)]
		public byte[] ActionBarSettings { get; internal set; }

		[KnownSize(0x14)]
		[WireMember(15)]
		public byte[] Techniques { get; internal set; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public LobbyCharacterData()
		{
			
		}
	}
}
