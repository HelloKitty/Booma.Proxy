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
	public sealed class SharedShipListEventPayload : PSOBBGamePacketPayloadServer, ISerializationEventListener
	{
		//Disable flags serialization so that the ship can get the 4 byte length and
		//handle writing the 4 bytes length
		/// <inheritdoc />
		public override bool isFlagsSerialized { get; } = false;

		//PSOBB sends 4 byte Flags with the entry count. We disable Flags though to steal the 4 bytes
		[SendSize(SendSizeAttribute.SizeType.Int32, 1)] //for some reason they send 1 less than the actual size 
		[WireMember(1)]
		private MenuListing[] _MenuListings { get; } //settable for removing the garbage entry

		/// <summary>
		/// All the menu listings sent in the packet.
		/// </summary>
		public IEnumerable<MenuListing> MenuListings => _MenuListings;

		/// <summary>
		/// Only the ship menu listings sent in the packet.
		/// </summary>
		public IEnumerable<MenuListing> Ships => _MenuListings.Skip(1);

		//TODO: Failing test cases for mismatch size. The reason it is happening is public Teth sends 8 extra padding bytes that it doesn't need.

		//Serializer ctor
		private SharedShipListEventPayload()
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
			//We no longer remove the the garbage entry
			//It is up to the consumer of the DTO to decide what they want.
		}
	}
}
