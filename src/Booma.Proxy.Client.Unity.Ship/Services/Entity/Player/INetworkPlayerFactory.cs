using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for a factory that creates <see cref="INetworkPlayer"/>s.
	/// </summary>
	public interface INetworkPlayerFactory : INetworkEntityFactory<INetworkPlayer>
	{
		/// <summary>
		/// Creates a new <see cref="INetworkPlayer"/> instance for the local player with the associated assigned id.
		/// This is just the data model and the networking component reference for the player. It is not the character nor
		/// the character data.
		/// </summary>
		/// <returns></returns>
		INetworkPlayer CreateLocalPlayer();
	}
}
