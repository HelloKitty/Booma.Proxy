using System;
using System.Collections.Generic;
using System.Text;

namespace Booma
{
	public interface IReadonlyEntityGuidMappable<TValue> : IReadOnlyDictionary<int, TValue>
	{
		
	}
}
