using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Contract for types that have a fixed size.
	/// </summary>
	public interface IBodySizable
	{
		/// <summary>
		/// The size of the body.
		/// </summary>
		int BodySize { get; }
	}
}
