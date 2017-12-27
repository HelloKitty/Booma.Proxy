using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// The collection of network entities that are known about.
	/// </summary>
	public interface INetworkEntityCollection<out TEntityType> : IEnumerable<TEntityType>
		where TEntityType : INetworkEntity
	{
		/// <summary>
		/// The networked entities.
		/// </summary>
		IEnumerable<TEntityType> Entities { get; }

		/// <summary>
		/// Returns the <typeparamref name="TEntityType"/> with the id.
		/// Or null if the entity doesn't exist.
		/// </summary>
		/// <param name="id">The id to check for.</param>
		/// <returns>The <typeparamref name="TEntityType"/> with the id or null.</returns>
		TEntityType this[int id] { get; }

		/// <summary>
		/// Indicates if it contains the <see cref="id"/> key value.
		/// </summary>
		/// <param name="id">The id to check for.</param>
		/// <returns>True if the collection contains the ID.</returns>
		bool ContainsId(int id);
	}
}
