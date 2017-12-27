using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for a factory that creates <typeparamref name="TEntityType"/>s.
	/// </summary>
	public interface INetworkEntityFactory<out TEntityType>
		where TEntityType : INetworkEntity
	{
		/// <summary>
		/// Creates a new <typeparamref name="TEntityType"/> instance with the associated <see cref="id"/>.
		/// This is just the data model and networking component reference for the entity.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns>A new <typeparamref name="TEntityType"/>.</returns>
		TEntityType CreateEntity(int id);

		/// <summary>
		/// Creates a new <typeparamref name="TEntityType"/> instance with the associated <see cref="id"/>.
		/// This is just the data model and networking component reference for the entity.
		/// Uses the provided rotation and position data.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="position">The position to create the <see cref="GameObject"/> at.</param>
		/// <param name="rotation">The rotation to set for the created <see cref="GameObject"/>.</param>
		/// <returns>A new <typeparamref name="TEntityType"/>.</returns>
		TEntityType CreateEntity(int id, Vector3 position, Quaternion rotation);
	}
}
