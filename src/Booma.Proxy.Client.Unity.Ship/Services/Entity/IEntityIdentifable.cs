using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for types that are identifable for their entity.
	/// </summary>
	public interface IEntityIdentifable
	{
		/// <summary>
		/// The unique identifier for the entity.
		/// </summary>
		int EntityGuid { get; }
	}
}
