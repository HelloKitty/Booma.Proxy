using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	//TODO: This is a temporary implementation of the item collection. When more is known implement it properly.
	/// <summary>
	/// Simple implementation of the <see cref="INetworkPlayerCollection"/>.
	/// </summary>
	public sealed class DefaultNetworkItemCollection : INetworkEntityCollection<INetworkItem>, INetworkEntityRegistery<INetworkItem>
	{
		/// <inheritdoc />
		public IEnumerable<INetworkItem> Entities => this;

		/// <inheritdoc />
		public INetworkItem this[int id] => ManagedItemMap[id];

		/// <summary>
		/// Internally managed item map that provided quick lookup for client id to player.
		/// </summary>
		private IDictionary<int, INetworkItem> ManagedItemMap { get; }

		public DefaultNetworkItemCollection()
		{
			//TODO: Do we need thread safety?
			ManagedItemMap = new Dictionary<int, INetworkItem>(15);
		}

		/// <inheritdoc />
		public bool ContainsId(int id)
		{
			return ManagedItemMap.ContainsKey(id);
		}

		/// <inheritdoc />
		public IEnumerator<INetworkItem> GetEnumerator()
		{
			return ManagedItemMap.Values.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <inheritdoc />
		public void AddEntity(int id, INetworkItem entity)
		{
			if(ManagedItemMap.ContainsKey(id))
				throw new InvalidOperationException($"Tried to add item with Id: {id} but that id is already associated. Details: {entity}");

			ManagedItemMap.Add(id, entity);
		}

		/// <inheritdoc />
		public INetworkItem RemoveEntity(int id)
		{
			if(!ManagedItemMap.ContainsKey(id))
				return null;

			INetworkItem item = ManagedItemMap[id];

			ManagedItemMap.Remove(id);

			return item;
		}
	}
}
