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
	public sealed class CharacterCharacterUpdateResponsePayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// The slot being selected.
		/// </summary>
		[WireMember(1)]
		public int SlotSelected { get; }

		[WireMember(2)]
		public PlayerCharacterDataModel CharacterData { get; }

		//serializer ctor
		private CharacterCharacterUpdateResponsePayload()
		{
			
		}
	}
}
