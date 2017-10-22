using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for a room.
	/// </summary>
	public interface IRoom : IPlayerEnterable
	{
		/// <summary>
		/// The ID associated with the room.
		/// </summary>
		short RoomId { get; }
	}
}
