using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Proxy
{
	//TODO: Doc
	public class WorldObjectToEntityDictionary : ConcurrentDictionary<GameObject, int>, IReadonlyWorldObjectToEntityMappable, IWorldObjectToEntityMappable
	{
		public WorldObjectToEntityDictionary()
			: base()
		{

		}

		public WorldObjectToEntityDictionary(int capacity)
			: base(4, capacity)
		{

		}

		public WorldObjectToEntityDictionary(IDictionary<GameObject, int> dictionary)
			: base(dictionary)
		{

		}
	}
}
