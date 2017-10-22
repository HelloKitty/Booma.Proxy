using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for any collection of rooms.
	/// </summary>
	public interface IRoomCollection : IEnumerable<IRoom> //collection of rooms
	{
		/// <summary>
		/// The default room of the collection.
		/// (Ex. Could be the first room, or just the only room etc).
		/// </summary>
		IRoom DefaultRoom { get; }
	}
}
