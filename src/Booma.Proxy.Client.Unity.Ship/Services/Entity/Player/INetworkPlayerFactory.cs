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
	public interface INetworkPlayerFactory
	{
		/// <summary>
		/// Creates a new <see cref="INetworkPlayer"/> instance with the associated <see cref="id"/>.
		/// This is just the data model and networking component reference for the player. It is not the character
		/// nor the character data.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns>A new <see cref="INetworkPlayer"/>.</returns>
		INetworkPlayer CreatePlayer(int id);

		/// <summary>
		/// Creates a new <see cref="INetworkPlayer"/> instance with the associated <see cref="id"/>.
		/// This is just the data model and networking component reference for the player. It is not the character
		/// nor the character data.
		/// 
		/// Uses the provided rotation and position data.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="position">The position to create the <see cref="GameObject"/> at.</param>
		/// <param name="rotation">The rotation to set for the created <see cref="GameObject"/>.</param>
		/// <returns>A new <see cref="INetworkPlayer"/>.</returns>
		INetworkPlayer CreatePlayer(int id, Vector3 position, Quaternion rotation);

		/// <summary>
		/// Creates a new <see cref="INetworkPlayer"/> instance for the local player with the associated assigned id.
		/// This is just the data model and the networking component reference for the player. It is not the character nor
		/// the character data.
		/// </summary>
		/// <returns></returns>
		INetworkPlayer CreateLocalPlayer();
	}
}
