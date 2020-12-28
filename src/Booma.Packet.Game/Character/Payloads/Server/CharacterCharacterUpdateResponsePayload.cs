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
	/// Payload sent when character preview data is requested.
	/// Contains the slot this update is for and the associated
	/// character data.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BB_CHARACTER_UPDATE_TYPE)]
	public sealed partial class CharacterCharacterUpdateResponsePayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// The slot being selected.
		/// </summary>
		[WireMember(1)]
		public byte SlotSelected { get; internal set; }

		[KnownSize(3)]
		[WireMember(2)]
		internal byte[] unused { get; set; }

		/// <summary>
		/// The character data for <see cref="SlotSelected"/>.
		/// </summary>
		[WireMember(3)]
		public PlayerCharacterDataModel CharacterData { get; internal set; }

		public CharacterCharacterUpdateResponsePayload(byte slotSelected, PlayerCharacterDataModel characterData) 
			: this()
		{
			SlotSelected = slotSelected;

			//TODO: Support sending null array for fixed size.
			this.unused = new byte[3];
			CharacterData = characterData;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterCharacterUpdateResponsePayload()
			: base(GameNetworkOperationCode.BB_CHARACTER_UPDATE_TYPE)
		{
			
		}
	}
}
