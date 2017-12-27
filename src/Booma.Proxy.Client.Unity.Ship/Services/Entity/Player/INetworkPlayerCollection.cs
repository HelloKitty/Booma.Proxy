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
	public interface INetworkPlayerCollection : INetworkEntityCollection<INetworkPlayer>
	{
		/// <summary>
		/// The local player.
		/// </summary>
		INetworkPlayer Local { get; }

		/// <summary>
		/// The networked player's excluding the <see cref="Local"/> player.
		/// </summary>
		IEnumerable<INetworkPlayer> ExcludingLocal { get; }
	}
}
