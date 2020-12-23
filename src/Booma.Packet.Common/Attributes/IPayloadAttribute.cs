using System;
using System.Collections.Generic;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Contract for payload attributes.
	/// </summary>
	public interface IPayloadAttribute
	{
		/// <summary>
		/// The base payload attribute.
		/// </summary>
		Type BaseType { get; }
	}
}
