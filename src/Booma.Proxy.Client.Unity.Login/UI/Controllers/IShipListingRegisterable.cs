using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for Types that allow for ship registerations.
	/// </summary>
	public interface IShipListingRegisterable
	{
		/// <summary>
		/// Registers a ship listing based on the provided
		/// <see cref="MenuListing"/> <see cref="model"/>.
		/// </summary>
		/// <param name="model">The ship listing model.</param>
		void RegisterShip(MenuListing model);
	}
}
