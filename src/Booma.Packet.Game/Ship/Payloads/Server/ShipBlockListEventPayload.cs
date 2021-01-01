using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	//TODO: Sometimes A0 is used and sometimes 07 is used. We should create a DTO for both.
	/// <summary>
	/// Contains the block list for menu rendering.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BLOCK_LIST_TYPE)]
	public sealed partial class ShipBlockListEventPayload : PSOBBGamePacketPayloadServer, ISerializationEventListener
	{
		//Disable flags serialization so that the ship can get the 4 byte length and
		//handle writing the 4 bytes length
		/// <inheritdoc />
		public override bool isFlagsSerialized { get; } = false;

		/// <summary>
		/// Ship list sends with a hidden entry at the top for some reason.
		/// This is a default entry that can be used.
		/// </summary>
		private static MenuListing DefaultHiddenEntry { get; } = new MenuListing(new MenuItemIdentifier(uint.MaxValue, uint.MaxValue), 0, "Booma");

		//PSOBB sends 4 byte Flags with the entry count. We disable Flags though to steal the 4 bytes
		[SendSize(PrimitiveSizeType.Int32)] //for some reason they send 1 less than the actual size 
		[WireMember(1)]
		internal MenuListing[] _MenuListings { get; set; } //settable for removing the garbage entry

		/// <summary>
		/// All the menu listings sent in the packet.
		/// </summary>
		public IEnumerable<MenuListing> MenuListings => EnumerateListings();

		private IEnumerable<MenuListing> EnumerateListings()
		{
			foreach(var entry in _MenuListings)
				yield return entry;

			yield return LastMenuListing;
		}

		/// <summary>
		/// Only the ship menu listings sent in the packet.
		/// </summary>
		public IEnumerable<MenuListing> Blocks => EnumerateShips();

		private IEnumerable<MenuListing> EnumerateShips()
		{
			//First hidden entry is skipped.
			return EnumerateListings()
				.Skip(1);
		}

		//TODO: Failing test cases for mismatch size. The reason it is happening is public Teth sends 8 extra padding bytes that it doesn't need.
		[WireMember(2)]
		internal MenuListing LastMenuListing { get; set; }

		/// <summary>
		/// Creates a new block list packet with the provided ships <see cref="shipList"/>.
		/// </summary>
		/// <param name="shipList">The list of ships.</param>
		/// <param name="blockList"></param>
		/// <param name="hiddenMenuHeader">Sets the hidden menu header/option.</param>
		public ShipBlockListEventPayload([NotNull] MenuListing[] blockList, [NotNull] MenuListing hiddenMenuHeader)
			: this()
		{
			if(blockList == null) throw new ArgumentNullException(nameof(blockList));

			//Special cast for ship list entry of 1
			if(blockList.Length == 0)
			{
				_MenuListings = Array.Empty<MenuListing>();
				LastMenuListing = hiddenMenuHeader;
			}
			else
			{
				//Hidden member is first in the array, then we append ship list
				//and finally the last element is stripped via Take
				_MenuListings = new MenuListing[] { hiddenMenuHeader }
					.Concat(blockList)
					.Take(blockList.Length) //this trims off the last element (length - 1 trims off 2)
					.ToArray();

				LastMenuListing = blockList[blockList.Length - 1];
			}
		}

		/// <summary>
		/// Creates a new ship list packet with the provided ships <see cref="shipList"/>.
		/// </summary>
		/// <param name="shipList">The list of ships.</param>
		public ShipBlockListEventPayload([NotNull] MenuListing[] shipList)
			: this(shipList, DefaultHiddenEntry)
		{

		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public ShipBlockListEventPayload()
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
