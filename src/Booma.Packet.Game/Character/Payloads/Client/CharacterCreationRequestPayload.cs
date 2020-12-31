using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/* Blue Burst packet for creating/updating a character as well as for the
   previews sent for the character select screen. 
	typedef struct bb_char_preview
	{
		bb_pkt_hdr_t hdr;
		uint8_t slot;
		uint8_t unused[3];
		sylverant_bb_mini_char_t data;
	}
	PACKED bb_char_preview_pkt;*/

	/// <summary>
	/// Message sent by the client when they request to create a new character.
	/// (Possibly also sent when they want to save character data? Haven't checked yet).
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.BB_CHARACTER_UPDATE_TYPE)]
	public sealed partial class CharacterCreationRequestPayload : PSOBBGamePacketPayloadClient
	{
		/// <summary>
		/// The slot being selected.
		/// </summary>
		[WireMember(1)]
		public byte SlotSelected { get; internal set; }

		[KnownSize(3)]
		[WireMember(2)]
		internal byte[] unused { get; set; } = Array.Empty<byte>();

		/// <summary>
		/// The character data for <see cref="SlotSelected"/>.
		/// </summary>
		[WireMember(3)]
		public PlayerCharacterDataModel CharacterData { get; internal set; }

		public CharacterCreationRequestPayload(byte slotSelected, PlayerCharacterDataModel characterData) 
			: this()
		{
			SlotSelected = slotSelected;
			CharacterData = characterData;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterCreationRequestPayload()
			: base(GameNetworkOperationCode.BB_CHARACTER_UPDATE_TYPE)
		{
			
		}
	}
}
