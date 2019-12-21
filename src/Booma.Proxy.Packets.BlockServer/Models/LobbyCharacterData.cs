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
		public CharacterStats Stats { get; }

		/*uint16_t unk1;
		uint32_t unk2[2];*/

		[WireMember(2)]
		private ushort unk1 { get; }

		[WireMember(3)]
		private ulong unk2 { get; }

		[WireMember(4)]
		public CharacterProgress Progress { get; }

		[WireMember(5)]
		public int Meseta { get; }

		//TODO: Is this just the guild card as a string?
		/// <summary>
		/// The guild card.
		/// </summary>
		[KnownSize(16)]
		[WireMember(6)]
		public string GuildCard { get; }

		//TODO: What is this?
		[KnownSize(2)]
		[WireMember(7)]
		private uint[] unk3 { get; }

		/// <summary>
		/// The special character data such as name color
		/// or NPC skin.
		/// </summary>
		[WireMember(8)]
		public CharacterSpecialCustomInfo Special { get; }

		/// <summary>
		/// Section ID of the character.
		/// </summary>
		[WireMember(9)]
		public SectionId SectionId { get; }

		/// <summary>
		/// The class/race for the character.
		/// </summary>
		[WireMember(10)]
		public CharacterClassRace ClassRace { get; }

		//TODO: What is this?
		/// <summary>
		/// Character version data.
		/// </summary>
		[WireMember(11)]
		public CharacterVersionData VersionData { get; }

		/// <summary>
		/// Character customization information/data
		/// (Ex. Hair, Costume)
		/// </summary>
		[WireMember(12)]
		public CharacterCustomizationInfo CustomizationInfo { get; }

		/// <summary>
		/// The name of the character.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[KnownSize(16)] //TODO: Destiny only sends 15 char for char name. Other servers use 16.
		[WireMember(13)]
		public string CharacterName { get; }

		//TODO: No idea what this is
		[KnownSize(0xE8)]
		[WireMember(14)]
		public byte[] Config { get; }

		[KnownSize(0x14)]
		[WireMember(15)]
		public byte[] Techniques { get; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private LobbyCharacterData()
		{
			
		}
	}
}
