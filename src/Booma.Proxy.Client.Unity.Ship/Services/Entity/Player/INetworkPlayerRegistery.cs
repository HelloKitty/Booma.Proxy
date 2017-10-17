using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	//TODO: Implement
	public interface INetworkPlayerRegistery
	{
		/// <summary>
		/// Adds a new <see cref="INetworkPlayer"/> with the provided id.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="player"></param>
		void AddPlayer(int id, INetworkPlayer player);

		/// <summary>
		/// Tries to remove a <see cref="INetworkPlayer"/> with the provided id.
		/// Returns null if not found.
		/// </summary>
		/// <param name="id">The id of the player to remove.</param>
		/// <returns>The removed player or null if none was found.</returns>
		INetworkPlayer RemovePlayer(int id);
	}
}
