using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Proxy
{
	//TODO: Figure out how we should implement this
	/// <summary>
	/// Enumeration of all entity types.
	/// </summary>
	public enum EntityType : byte
	{
		/// <summary>
		/// Represents a typeless entity.
		/// </summary>
		None = 0,

		/// <summary>
		/// Represents a player entity.
		/// </summary>
		Player = 1,

		/// <summary>
		/// Represents an item entity.
		/// </summary>
		Item = 2
	}
}
