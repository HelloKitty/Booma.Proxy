using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IRoomQueryable
	{
		/// <summary>
		/// Queries for the room Id a provided <see cref="INetworkPlayer"/>'s id
		/// is in.
		/// </summary>
		/// <param name="playerId">The player.</param>
		/// <returns>The id of the room if it's in a room. 0 if not.</returns>
		short RoomIdForPlayerById(int playerId);
	}
}
