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

	public sealed class ShipListingDataRecievedEventArgs
	{
		/// <summary>
		/// The identifier for the menu option.
		/// </summary>
		public MenuItemIdentifier Identifier { get; }

		public string ShipName { get; }

		/// <inheritdoc />
		public ShipListingDataRecievedEventArgs([NotNull] MenuItemIdentifier identifier, [NotNull] string shipName)
		{
			Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
			ShipName = shipName ?? throw new ArgumentNullException(nameof(shipName));
		}
	}
}
