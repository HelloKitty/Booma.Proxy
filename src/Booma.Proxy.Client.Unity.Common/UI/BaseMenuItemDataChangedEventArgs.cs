using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public class BaseMenuItemDataChangedEventArgs : EventArgs
	{
		/// <summary>
		/// The identifier for the menu option.
		/// </summary>
		public MenuItemIdentifier Identifier { get; }

		public string MenuItemName { get; }

		/// <inheritdoc />
		public BaseMenuItemDataChangedEventArgs(MenuItemIdentifier identifier, string menuItemName)
		{
			Identifier = identifier;
			MenuItemName = menuItemName;
		}
	}
}
