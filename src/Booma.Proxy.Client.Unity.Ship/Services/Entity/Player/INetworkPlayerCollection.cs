using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// The collection of network players that are known about.
	/// </summary>
	public interface INetworkPlayerCollection : IEnumerable<INetworkPlayer>
	{
		/// <summary>
		/// The networked players.
		/// </summary>
		IEnumerable<INetworkPlayer> Players { get; }

		/// <summary>
		/// Returns the <see cref="INetworkPlayer"/> with the id.
		/// Or null if the player doesn't exist.
		/// </summary>
		/// <param name="id">The id to check for.</param>
		/// <returns>The <see cref="INetworkPlayer"/> with the id or null.</returns>
		INetworkPlayer this[int id] { get; }

		/// <summary>
		/// Indicates if it contains the <see cref="id"/> key value.
		/// </summary>
		/// <param name="id">The id to check for.</param>
		/// <returns>True if the collection contains the ID.</returns>
		bool ContainsId(int id);
	}
}
