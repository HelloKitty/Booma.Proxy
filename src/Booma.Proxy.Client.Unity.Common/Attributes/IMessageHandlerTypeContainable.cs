using System;
using System.Collections.Generic;

namespace Booma
{
	public interface IMessageHandlerTypeContainable
	{
		IEnumerable<Type> AssemblyDefinedHandlerTyped { get; }
	}
}