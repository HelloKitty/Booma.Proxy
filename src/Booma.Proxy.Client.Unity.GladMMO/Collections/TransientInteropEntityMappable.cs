using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladMMO;

namespace Booma.Proxy
{
	public sealed class TransientInteropEntityMappable : IInteropEntityMappable
	{
		private ConcurrentDictionary<int, NetworkEntityGuid> InternalMap { get; } = new ConcurrentDictionary<int, NetworkEntityGuid>();

		public bool ContainsKey(int key)
		{
			return InternalMap.ContainsKey(key);
		}

		public void Add(int key, NetworkEntityGuid value)
		{
			InternalMap.TryAdd(key, value);
		}

		public NetworkEntityGuid this[int key]
		{
			get => InternalMap[key];
			set => InternalMap[key] = value;
		}

		bool IEntityCollectionRemovable<int>.RemoveEntityEntry(int entityGuid)
		{
			return InternalMap.TryRemove(entityGuid, out var val);
		}
	}
}
