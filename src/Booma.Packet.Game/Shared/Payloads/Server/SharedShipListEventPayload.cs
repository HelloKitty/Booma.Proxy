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
	public sealed partial class SharedShipListEventPayload : PSOBBGamePacketPayloadServer
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
		public IEnumerable<MenuListing> Ships => EnumerateShips();

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
		/// Creates a new ship list packet with the provided ships <see cref="shipList"/>.
		/// </summary>
		/// <param name="shipList">The list of ships.</param>
		/// <param name="hiddenMenuHeader">Sets the hidden menu header/option.</param>
		public SharedShipListEventPayload([NotNull] MenuListing[] shipList, [NotNull] MenuListing hiddenMenuHeader) 
			: this()
		{
			if (shipList == null) throw new ArgumentNullException(nameof(shipList));

			//Special cast for ship list entry of 1
			if (shipList.Length == 0)
			{
				_MenuListings = Array.Empty<MenuListing>();
				LastMenuListing = hiddenMenuHeader;
			}
			else
			{
				//Hidden member is first in the array, then we append ship list
				//and finally the last element is stripped via Take
				_MenuListings = new MenuListing[] { hiddenMenuHeader }
					.Concat(shipList)
					.Take(shipList.Length - 1)
					.ToArray();

				LastMenuListing = shipList[shipList.Length - 1];
			}
		}

		/// <summary>
		/// Creates a new ship list packet with the provided ships <see cref="shipList"/>.
		/// </summary>
		/// <param name="shipList">The list of ships.</param>
		public SharedShipListEventPayload([NotNull] MenuListing[] shipList)
			: this(shipList, DefaultHiddenEntry)
		{

		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public SharedShipListEventPayload()
			: base(GameNetworkOperationCode.SHIP_LIST_TYPE)
		{
			
		}
	}
}
