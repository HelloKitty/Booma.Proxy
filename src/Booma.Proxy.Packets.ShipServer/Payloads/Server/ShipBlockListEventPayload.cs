using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//TODO: Sometimes A0 is used and sometimes 07 is used. We should create a DTO for both.
	/// <summary>
	/// Contains the block list for menu rendering.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BLOCK_LIST_TYPE)]
	public sealed class ShipBlockListEventPayload : PSOBBGamePacketPayloadServer, ISerializationEventListener
	{
		//Disable flags serialization so that the ship can get the 4 byte length and
		//handle writing the 4 bytes length
		/// <inheritdoc />
		public override bool isFlagsSerialized { get; } = false;

		//They include ShipSelect and Ship name in this listing
		//PSOBB sends 4 byte Flags with the entry count. We disable Flags though to steal the 4 bytes
		[SendSize(SendSizeAttribute.SizeType.Int32, 1)] //for some reason they send 1 less than the actual size 
		[WireMember(1)]
		private MenuListing[] _Blocks { get; set; } //settable for removing the garbage entry

		/// <summary>
		/// The ship menu models.
		/// </summary>
		public IEnumerable<MenuListing> Blocks => _Blocks;

		//TODO: Failing test cases for mismatch size. The reason it is happening is public Teth sends 8 extra padding bytes that it doesn't need.

		//Serializer ctor
		private ShipBlockListEventPayload()
		{
			
		}

		/// <inheritdoc />
		public void OnBeforeSerialization()
		{

		}

		/// <inheritdoc />
		public void OnAfterDeserialization()
		{
			//Remove the first entry, it's garbage
			//_Blocks = _Blocks.Skip(1).ToArray();
		}
	}
}
