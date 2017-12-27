using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for entities that are networked.
	/// </summary>
	public interface INetworkEntity
	{
		/// <summary>
		/// The network transform associated with the entity.
		/// </summary>
		INetworkEntityTransform Transform { get; }

		/// <summary>
		/// The identifier
		/// associated with the entity.
		/// </summary>
		IEntityIdentity Identity { get; }

		/// <summary>
		/// Despawn the player.
		/// </summary>
		void Despawn();
	}
}
