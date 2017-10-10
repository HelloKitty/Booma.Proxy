using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/*typedef struct bb_ship_list {
    bb_pkt_hdr_t hdr;           The flags field says how many entries
	struct {

		uint32_t menu_id;
		uint32_t item_id;
		uint16_t flags;
		uint16_t name[0x11];
		}
		entries[0];
	} PACKED bb_ship_list_pkt;*/

	/// <summary>
	/// Contains the ship list for menu rendering.
	/// </summary>
	[WireDataContract]
	[LoginServerPacketPayload(LoginNetworkOperationCodes.SHIP_LIST_TYPE)]
	public sealed class LoginShipListEventPayload : PSOBBLoginPacketPayloadServer
	{
		[WireMember(1)]
		private ShipListing[] _Ships { get; }

		/// <summary>
		/// The ship menu models.
		/// </summary>
		public IEnumerable<ShipListing> Ships => _Ships;

		//Serializer ctor
		private LoginShipListEventPayload()
		{
			
		}
	}
}
