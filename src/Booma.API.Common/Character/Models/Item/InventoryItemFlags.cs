using System;
using System.Collections.Generic;
using System.Text;

namespace Booma
{
	[Flags]
	public enum InventoryItemFlags : uint
	{
		None = 0,

		//I think this is banked lol
		/// <summary>
		/// Indicates that the item is in the bank (maybe?).
		/// </summary>
		Banked = 1 << 0,

		/// <summary>
		/// Flag indicates that the item is equipped.
		/// </summary>
		Equipped = 1 << 3,
	}
}
