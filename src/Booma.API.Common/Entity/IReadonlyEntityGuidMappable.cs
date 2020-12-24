using System;
using System.Collections.Generic;
using System.Text;

namespace Booma.Proxy
{
	public interface IReadonlyEntityGuidMappable<TValue> : IReadOnlyDictionary<int, TValue>
	{
		
	}
}
