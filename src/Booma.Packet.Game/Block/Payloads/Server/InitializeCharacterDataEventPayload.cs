using System;
using System.Collections.Generic;
using System.Text;
using Booma;
using FreecraftCore.Serializer;

namespace Booma
{
	//TODO: Document and finish implementing this packet.
	/// <summary>
	/// Packet sent by a block initially that contains all the menu information for a lobbies.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BB_FULL_CHARACTER_TYPE)]
	public sealed partial class InitializeCharacterDataEventPayload : PSOBBGamePacketPayloadServer
	{
		[WireMember(1)]
		public CharacterInventoryData Inventory { get; internal set; }

		[WireMember(2)]
		public LobbyCharacterData CharacterData { get; internal set; }

		//See: https://github.com/Sylverant/libsylverant/blob/206b44f906054c081f47627e546ea19dc00322b4/include/sylverant/characters.h#L184
		[KnownSize(16)]
		[WireMember(3)]
		internal byte[] unk1 { get; set; } = Array.Empty<byte>();

		//TODO: Figure out these flags and enumerate them
		[WireMember(4)]
		public uint Options { get; internal set; }

		//TODO: What is this?
		[KnownSize(520)]
		[WireMember(5)]
		public byte[] QuestData1 { get; internal set; } = Array.Empty<byte>();

		[WireMember(6)]
		public CharacterBankData BankData { get; internal set; }

		[WireMember(7)]
		public GuildCardEntry GuildCard { get; internal set; }

		[WireMember(8)]
		internal ushort unk2 { get; set; }

		[KnownSize(1248)]
		[WireMember(10)]
		public byte[] SymbolChat { get; internal set; } = Array.Empty<byte>();

		[KnownSize(2624)]
		[WireMember(11)]
		public byte[] Shortcuts { get; internal set; } = Array.Empty<byte>();

		[DontTerminate]
		[Encoding(EncodingType.UTF16)]
		[KnownSize(172)]
		[WireMember(12)]
		public string AutoReply { get; internal set; } = String.Empty;

		[DontTerminate]
		[Encoding(EncodingType.UTF16)]
		[KnownSize(172)]
		[WireMember(13)]
		public string InfoBoard { get; internal set; } = String.Empty;

		//See: https://github.com/Sylverant/libsylverant/blob/206b44f906054c081f47627e546ea19dc00322b4/include/sylverant/characters.h#L201
		[KnownSize(28)]
		[WireMember(14)]
		internal byte[] unk3 { get; set; } = Array.Empty<byte>();

		[KnownSize(320)]
		[WireMember(15)]
		public byte[] ChallengeData { get; internal set; } = Array.Empty<byte>();

		[KnownSize(40)]
		[WireMember(16)]
		public byte[] TechMenu { get; internal set; } = Array.Empty<byte>();

		//See: https://github.com/Sylverant/libsylverant/blob/206b44f906054c081f47627e546ea19dc00322b4/include/sylverant/characters.h#L204
		[KnownSize(44)]
		[WireMember(14)]
		internal byte[] unk4 { get; set; } = Array.Empty<byte>();

		[KnownSize(88)]
		[WireMember(15)]
		public byte[] QuestData2 { get; internal set; } = Array.Empty<byte>();

		[WireMember(16)]
		public CharacterOptionsConfiguration OptionsConfig { get; internal set; }

		public InitializeCharacterDataEventPayload(CharacterInventoryData inventory, LobbyCharacterData characterData, uint options, CharacterBankData bankData, GuildCardEntry guildCard, ushort unk2, CharacterOptionsConfiguration optionsConfig) 
			: this()
		{
			Inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
			CharacterData = characterData ?? throw new ArgumentNullException(nameof(characterData));
			Options = options;
			BankData = bankData ?? throw new ArgumentNullException(nameof(bankData));
			GuildCard = guildCard ?? throw new ArgumentNullException(nameof(guildCard));
			this.unk2 = unk2;
			OptionsConfig = optionsConfig ?? throw new ArgumentNullException(nameof(optionsConfig));
		}

		public InitializeCharacterDataEventPayload() 
			: base(GameNetworkOperationCode.BB_FULL_CHARACTER_TYPE)
		{

		}
	}
}
