using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	//TODO: Implement
	public interface INetworkPlayer
	{
		/// <summary>
		/// The network transform associated with the player.
		/// </summary>
		INetworkEntityTransform Transform { get; }
	}
}
