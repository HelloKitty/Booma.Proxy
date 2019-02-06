using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Context for creating a remote player.
	/// </summary>
	public sealed class RemotePlayerWorldRepresentationCreationContext : IEntityIdentifable
	{
		//We only need the guid. The factory should know where the player is.
		/// <summary>
		/// Data contains spawn information for the remote player.
		/// </summary>
		public int EntityGuid { get; }

		/// <inheritdoc />
		public RemotePlayerWorldRepresentationCreationContext(int entityId)
		{
			if(Booma.EntityGuid.GetEntityType(entityId) != EntityType.Player)
				throw new ArgumentException($"Cannot create: {nameof(LocalPlayerWorldRepresentationCreationContext)} with guid with {nameof(EntityType)}: {Booma.EntityGuid.GetEntityType(entityId)}", nameof(entityId));

			EntityGuid = entityId;
		}
	}
}
