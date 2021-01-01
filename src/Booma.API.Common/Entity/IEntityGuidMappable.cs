using System;
using System.Collections.Generic;
using System.Text;

namespace Booma
{
	public interface IEntityGuidMappable<TValue> : IDictionary<int, TValue>, IEntityCollectionRemovable
	{
		
	}
}
