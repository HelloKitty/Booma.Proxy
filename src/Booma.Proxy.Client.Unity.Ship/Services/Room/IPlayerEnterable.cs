using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IPlayerEnterable
	{
		/// <summary>
		/// Tries to enter the enterable.
		/// </summary>
		/// <param name="player">The player trying to enter.</param>
		/// <returns>True if the player could enter.</returns>
		bool TryEnter(INetworkPlayer player);

		/// <summary>
		/// Tries to exit the enterable.
		/// </summary>
		/// <param name="player">The player trying to exit.</param>
		/// <returns>True if the player could exit.</returns>
		bool TryExit(INetworkPlayer player);
	}
}
