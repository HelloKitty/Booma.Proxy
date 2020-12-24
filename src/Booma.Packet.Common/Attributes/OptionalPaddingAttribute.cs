using System;
using System.Collections.Generic;
using System.Text;

namespace Booma.Attributes
{
	/// <summary>
	/// Indicates that a serialized field represents optional padding that isn't strictly
	/// required. Some PSO emulation implementations differ in padding and packet sizes due to uneeded padding.
	/// This padding is added but also noted to pass serialization tests against packet captures.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class OptionalPaddingAttribute : Attribute
	{

		public OptionalPaddingAttribute()
		{
			
		}
	}
}
