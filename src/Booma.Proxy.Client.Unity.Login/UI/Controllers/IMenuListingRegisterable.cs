using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for Types that allow for menu registerations.
	/// </summary>
	public interface IMenuListingRegisterable
	{
		/// <summary>
		/// Registers a menu item listing based on the provided
		/// <see cref="MenuListing"/> <see cref="model"/>.
		/// </summary>
		/// <param name="model">The menu listing model.</param>
		void RegisterMenuItem(MenuListing model);
	}
}
