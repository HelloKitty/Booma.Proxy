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
	[GameServerPacketPayload(GameNetworkOperationCode.SHIP_LIST_TYPE)]
	public sealed class LoginShipListEventPayload : PSOBBGamePacketPayloadServer, ISerializationEventListener
	{
		//Disable flags serialization so that the ship can get the 4 byte length and
		//handle writing the 4 bytes length
		/// <inheritdoc />
		public override bool isFlagsSerialized { get; } = false;

		//PSOBB sends 4 byte Flags with the entry count. We disable Flags though to steal the 4 bytes
		[SendSize(SendSizeAttribute.SizeType.Int32, 1)] //for some reason they send 1 less than the actual size 
		[WireMember(1)]
		private MenuListing[] _Ships { get; set; } //settable for removing the garbage entry

		/// <summary>
		/// The ship menu models.
		/// </summary>
		public IEnumerable<MenuListing> Ships => _Ships;

		//Serializer ctor
		private LoginShipListEventPayload()
		{
			
		}

		/// <inheritdoc />
		public void OnBeforeSerialization()
		{
			//TODO: Deal with the bullshit the server adds for some reason
		}

		/// <inheritdoc />
		public void OnAfterDeserialization()
		{
			//Remove the first entry, it's garbage
			_Ships = _Ships.Skip(1).ToArray();
		}
	}
}
