using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	//TODO: Implement
	public interface INetworkPlayer : INetworkEntity
	{
		/// <summary>
		/// Indicates if the player is the local player.
		/// </summary>
		bool isLocalPlayer { get; }
	}
}
