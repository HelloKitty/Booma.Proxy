using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Proxy
{
	/// <summary>
	/// Generic dictionary with <see cref="NetworkEntityGuid"/> key types.
	/// </summary>
	/// <typeparam name="TValue">Value type.</typeparam>
	public class EntityGuidDictionary<TValue> : ConcurrentDictionary<int, TValue>, IReadonlyEntityGuidMappable<TValue>, IEntityGuidMappable<TValue>
	{
		public EntityGuidDictionary()
			: base()
		{

		}

		public EntityGuidDictionary(int capacity)
			: base(4, capacity)
		{

		}

		public EntityGuidDictionary(IDictionary<int, TValue> dictionary)
			: base(dictionary)
		{

		}

		/// <inheritdoc />
		public bool RemoveEntityEntry(int entityGuid)
		{
			return TryRemove(entityGuid, out var temp);
		}
	}
}
