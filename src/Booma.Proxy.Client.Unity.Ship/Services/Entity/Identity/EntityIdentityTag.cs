using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Component decorator for the <see cref="IEntityIdentity"/>
	/// allowing objects to query the world representation of an entity
	/// to get its identity.
	/// </summary>
	[Injectee]
	public sealed class EntityIdentityTag : MonoBehaviour, IEntityIdentity
	{
		/// <summary>
		/// Decorated identity.
		/// </summary>
		[Inject]
		private IEntityIdentity Identity { get; }

		/// <inheritdoc />
		public int EntityId => Identity.EntityId;

		/// <inheritdoc />
		public EntityType EntityType => Identity.EntityType;
	}
}
