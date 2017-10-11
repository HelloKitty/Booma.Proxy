using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Contains the ship list for menu rendering.
	/// </summary>
	[WireDataContract]
	[LoginServerPacketPayload(LoginNetworkOperationCodes.SHIP_LIST_TYPE)]
	public sealed class LoginShipListEventPayload : PSOBBLoginPacketPayloadServer
	{
		//Disable flags serialization so that the ship can get the 4 byte length and
		//handle writing the 4 bytes length
		/// <inheritdoc />
		public override bool isFlagsSerialized { get; } = false;

		//PSOBB sends 4 byte Flags with the entry count. We disable Flags though to steal the 4 bytes
		[SendSize(SendSizeAttribute.SizeType.Int32)] 
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
