using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Attribute marks a data-model that can/will be associated with an entity
	/// as non-transient. This means that when an entity goes out of world-view, or the scene changes, that
	/// entity data will be preserved.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class NonTransientEntityDataAttribute : Attribute
	{
		public NonTransientEntityDataAttribute()
		{
			
		}
	}
}
