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
		[SendSize(PrimitiveSizeType.Int32)] //for some reason they send 1 less than the actual size 
		[WireMember(1)]
		internal MenuListing[] _MenuListings { get; set; } //settable for removing the garbage entry

		/// <summary>
		/// All the menu listings sent in the packet.
		/// </summary>
		public IEnumerable<MenuListing> MenuListings => EnumerateMenuListings();

		private IEnumerable<MenuListing> EnumerateMenuListings()
		{
			foreach(var entry in _MenuListings)
				yield return entry;

			yield return LastMenuListing;
		}

		/// <summary>
		/// Only the block menu listings sent in the packet.
		/// </summary>
		public IEnumerable<MenuListing> Blocks => EnumerateBlocks(); 

		private IEnumerable<MenuListing> EnumerateBlocks()
		{
			//skip first, and last.
			//Last now being LastMenuListing
			foreach(var entry in _MenuListings.Skip(1))
				yield return entry;
		}

		//TODO: Failing test cases for mismatch size. The reason it is happening is public Teth sends 8 extra padding bytes that it doesn't need.
		[WireMember(2)]
		internal MenuListing LastMenuListing { get; set; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private ShipBlockListEventPayload()
			: base(GameNetworkOperationCode.BLOCK_LIST_TYPE)
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
