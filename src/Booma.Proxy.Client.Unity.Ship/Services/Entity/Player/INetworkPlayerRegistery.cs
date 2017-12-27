using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	//TODO: Implement
	public interface INetworkEntityRegistery<TEntityType>
		where TEntityType : INetworkEntity
	{
		/// <summary>
		/// Adds a new <typeparamref name="TEntityType"/> with the provided id.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="entity"></param>
		void AddEntity(int id, TEntityType entity);

		/// <summary>
		/// Tries to remove a <typeparamref name="TEntityType"/> with the provided id.
		/// Returns null if not found.
		/// </summary>
		/// <param name="id">The id of the player to remove.</param>
		/// <returns>The removed player or null if none was found.</returns>
		TEntityType RemoveEntity(int id);
	}
}
