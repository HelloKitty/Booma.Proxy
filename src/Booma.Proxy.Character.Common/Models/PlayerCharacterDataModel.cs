using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
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
		public CharacterProgress Progress { get; }

		//TODO: Is this just the guild card as a string?
		/// <summary>
		/// The guild card.
		/// </summary>
		[KnownSize(16)]
		[WireMember(2)]
		public string GuildCard { get; }

		//TODO: What is this?
		[KnownSize(2)]
		[WireMember(3)]
		private uint[] unk3 { get; }

		/// <summary>
		/// The special character data such as name color
		/// or NPC skin.
		/// </summary>
		[WireMember(4)]
		public CharacterSpecialCustomInfo Special { get; }

		/// <summary>
		/// Section ID of the character.
		/// </summary>
		[WireMember(5)]
		public SectionId SectionId { get; }

		/// <summary>
		/// The class/race for the character.
		/// </summary>
		[WireMember(6)]
		public CharacterClassRace ClassRace { get; }

		//TODO: What is this?
		/// <summary>
		/// Character version data.
		/// </summary>
		[WireMember(7)]
		public CharacterVersionData VersionData { get; }

		/// <summary>
		/// Character customization information/data
		/// (Ex. Hair, Costume)
		/// </summary>
		[WireMember(8)]
		public CharacterCustomizationInfo CustomizationInfo { get; }

		/// <summary>
		/// The name of the character.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[KnownSize(12)] //TODO: Destiny only sends 15 char for char name. Other servers use 16.
		[WireMember(9)]
		public string CharacterName { get; }

		/// <summary>
		/// The amount of time the character has played.
		/// </summary>
		[WireMember(10)]
		public ulong PlayedTime { get; }

		//Serializer ctor
		public PlayerCharacterDataModel()
		{
			
		}
	}
}
