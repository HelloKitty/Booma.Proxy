using System;
using System.Collections.Generic;

namespace Booma
{
	public interface IAssemblyTypeContainable
	{
		IEnumerable<Type> AllAssemblyTypes { get; }
	}
}