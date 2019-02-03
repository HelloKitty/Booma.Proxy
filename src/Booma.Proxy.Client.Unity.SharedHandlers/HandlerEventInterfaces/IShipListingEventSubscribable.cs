using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IShipListingEventSubscribable
	{
		/// <summary>
		/// Event fired when a ship listing is recieved.
		/// </summary>
		event EventHandler<ShipListingDataRecievedEventArgs> OnShipListingRecieved;

		/// <summary>
		/// Event that should be fired when a ship list has been fully recieved.
		/// </summary>
		event EventHandler OnShipListFinishedRecieving;
	}

	public sealed class ShipListingDataRecievedEventArgs : BaseMenuItemDataChangedEventArgs
	{
		/// <inheritdoc />
		public ShipListingDataRecievedEventArgs(MenuItemIdentifier identifier, string menuItemName) 
			: base(identifier, menuItemName)
		{

		}
	}
}
