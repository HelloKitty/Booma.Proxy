using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FreecraftCore.Serializer;

namespace Booma
{
	/*typedef struct bb_guildcard_data {
	uint8_t unk1[0x0114];
	struct {
		uint32_t guildcard;
		uint16_t name[0x18];
		uint16_t team[0x10];
		uint16_t desc[0x58];
		uint8_t reserved1;
		uint8_t language;
		uint8_t section;
		uint8_t ch_class;
	} blocked[29];
	uint8_t unk2[0x78];
	struct {
		uint32_t guildcard;
		uint16_t name[0x18];
		uint16_t team[0x10];
		uint16_t desc[0x58];
		uint8_t reserved1;
		uint8_t language;
		uint8_t section;
		uint8_t ch_class;
		uint32_t padding;
		uint16_t comment[0x58];
	} entries[104];
	uint8_t unk3[0x01BC];
} bb_gc_data_t;*/

	[WireDataContract]
	public sealed class GuildCardEntry
	{
		/*uint32_t guildcard;
		uint16_t name[0x18];
		uint16_t team[0x10];
		uint16_t desc[0x58];
		uint8_t reserved1;
		uint8_t language;
		uint8_t section;
		uint8_t ch_class;*/

		[WireMember(1)]
		public uint GuildCard { get; internal set; }

		[DontTerminate]
		[KnownSize(24)] //0x18
		[Encoding(EncodingType.UTF16)]
		[WireMember(2)]
		public string Name { get; internal set; }

		[DontTerminate]
		[KnownSize(16)] //0x10
		[Encoding(EncodingType.UTF16)]
		[WireMember(3)]
		public string TeamName { get; internal set; }

		[DontTerminate]
		[KnownSize(88)] //0x58
		[Encoding(EncodingType.UTF16)]
		[WireMember(4)]
		public string Description { get; internal set; }

		[WireMember(5)]
		internal byte unk1 { get; set; }

		//TODO: Enumerate language types
		[WireMember(6)]
		public byte Language { get; internal set; } = 1;

		[EnumSize(PrimitiveSizeType.Byte)]
		[WireMember(7)]
		public SectionId SectionId { get; internal set; }

		[EnumSize(PrimitiveSizeType.Byte)]
		[WireMember(8)]
		public CharacterClass ClassType { get; internal set; }

		public GuildCardEntry(uint guildCard, [NotNull] string name, [NotNull] string teamName,
			[NotNull] string description, byte language, SectionId sectionId, CharacterClass classType)
		{
			if (!Enum.IsDefined(typeof(SectionId), sectionId)) throw new InvalidEnumArgumentException(nameof(sectionId), (int) sectionId, typeof(SectionId));
			if(!Enum.IsDefined(typeof(CharacterClass), classType)) throw new InvalidEnumArgumentException(nameof(classType), (int)classType, typeof(CharacterClass));

			GuildCard = guildCard;
			Name = name ?? throw new ArgumentNullException(nameof(name));
			TeamName = teamName ?? throw new ArgumentNullException(nameof(teamName));
			Description = description ?? throw new ArgumentNullException(nameof(description));
			Language = language;
			SectionId = sectionId;
			ClassType = classType;
		}

		/// <summary>
		/// Creates an empty <see cref="GuildCardEntry"/>.
		/// </summary>
		/// <returns></returns>
		public static GuildCardEntry CreateEmpty()
		{
			return new GuildCardEntry(0, String.Empty, String.Empty, String.Empty, 0, SectionId.Viridia, CharacterClass.HUmar);
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public GuildCardEntry()
		{
			
		}
	}

	[WireDataContract]
	public sealed class GuildCardFriend
	{
		[WireMember(1)]
		public GuildCardEntry Data { get; internal set; }

		/*uint32_t padding;
		uint16_t comment[0x58];*/
		[WireMember(2)]
		internal int unk1 { get; set; }

		[DontTerminate]
		[Encoding(EncodingType.UTF16)]
		[KnownSize(88)] //0x58
		[WireMember(3)]
		public string Comment { get; internal set; }

		public GuildCardFriend([NotNull] GuildCardEntry data, [NotNull] string comment)
		{
			Data = data ?? throw new ArgumentNullException(nameof(data));
			Comment = comment ?? throw new ArgumentNullException(nameof(comment));
		}

		/// <summary>
		/// Creates an empty <see cref="GuildCardFriend"/>.
		/// </summary>
		/// <returns></returns>
		public static GuildCardFriend CreateEmpty()
		{
			return new GuildCardFriend(GuildCardEntry.CreateEmpty(), String.Empty);
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public GuildCardFriend()
		{
			
		}
	}

	//Based on: https://github.com/Sylverant/login_server/blob/12631a7035c68d6a8a99e3d0f524e7892554e6e7/src/player.h#L298
	/// <summary>
	/// Data model representing the entire guild card data.
	/// Sent in <see cref="CharacterGuildCardChunkResponsePayload"/> chunk payloads.
	/// </summary>
	[WireDataContract]
	public sealed class GuildCardData
	{
		/// <summary>
		/// Represents the max size of the blocked users.
		/// </summary>
		public const int BLOCKED_SIZE = 29;

		/// <summary>
		/// Represents the max size of the friend users.
		/// </summary>
		public const int FRIENDS_SIZE = 104;

		//uint8_t unk1[0x0114];
		[KnownSize(0x0114)]
		[WireMember(1)]
		internal byte[] unk1 { get; set; } = Array.Empty<byte>();

		[KnownSize(BLOCKED_SIZE)]
		[WireMember(2)]
		public GuildCardEntry[] Blocked { get; internal set; }

		//uint8_t unk2[0x78];
		[KnownSize(0x78)]
		[WireMember(3)]
		internal byte[] unk2 { get; set; } = Array.Empty<byte>();

		[KnownSize(FRIENDS_SIZE)]
		[WireMember(4)]
		public GuildCardFriend[] Friends { get; internal set; }

		//uint8_t unk3[0x01BC];
		[KnownSize(0x01BC)]
		[WireMember(5)]
		internal byte[] unk3 { get; set; } = Array.Empty<byte>();

		/// <summary>
		/// Represents an empty instance of the <see cref="GuildCardData"/>
		/// </summary>
		public static GuildCardData Empty { get; } = CreateEmpty();

		//Do not remove
		static GuildCardData()
		{
			
		}

		/// <summary>
		/// Creates a new guild card data with the following <see cref="Blocked"/> users and
		/// <see cref="Friends"/>.
		/// </summary>
		/// <param name="blocked">The blocked users.</param>
		/// <param name="friends">The friends.</param>
		public GuildCardData([NotNull] GuildCardEntry[] blocked, [NotNull] GuildCardFriend[] friends)
		{
			Blocked = blocked ?? throw new ArgumentNullException(nameof(blocked));
			Friends = friends ?? throw new ArgumentNullException(nameof(friends));

			if (Blocked.Length > BLOCKED_SIZE)
				throw new ArgumentException($"{nameof(blocked)} exceeds max size of: {BLOCKED_SIZE}", nameof(blocked));

			if (Friends.Length > FRIENDS_SIZE)
				throw new ArgumentException($"{nameof(friends)} exceeds max size of: {FRIENDS_SIZE}", nameof(friends));

			if (Blocked.Length != BLOCKED_SIZE)
				Blocked = Blocked
					.Concat(Enumerable.Repeat(GuildCardEntry.CreateEmpty(), Blocked.Length - BLOCKED_SIZE))
					.ToArray();

			if(Friends.Length != FRIENDS_SIZE)
				Friends = Friends
					.Concat(Enumerable.Repeat(GuildCardFriend.CreateEmpty(), Blocked.Length - FRIENDS_SIZE))
					.ToArray();
		}

		public static GuildCardData CreateEmpty()
		{
			return new GuildCardData(Enumerable.Repeat(GuildCardEntry.CreateEmpty(), 29).ToArray(), Enumerable.Repeat(GuildCardFriend.CreateEmpty(), 104).ToArray());
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public GuildCardData()
		{
			
		}
	}
}
