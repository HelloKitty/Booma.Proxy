﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Contract for types that contains the minimum
	/// information of a menu item.
	/// </summary>
	public interface IMenuItemIdentifiable
	{
		/// <summary>
		/// The id of the menu selecting from.
		/// </summary>
		uint MenuId { get; }

		/// <summary>
		/// The item id of the item on the menu
		/// being selected.
		/// </summary>
		uint ItemId { get; }
	}
}
