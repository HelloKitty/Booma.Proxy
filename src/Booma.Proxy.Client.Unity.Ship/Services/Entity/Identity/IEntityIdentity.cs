using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for types that implement entity
	/// identification data.
	/// </summary>
	public interface IEntityIdentity
	{
		/// <summary>
		/// The ID for the entity.
		/// </summary>
		int EntityId { get; }

		/// <summary>
		/// The entity's <see cref="EntityType"/>.
		/// </summary>
		EntityType EntityType { get; }
	}
}
