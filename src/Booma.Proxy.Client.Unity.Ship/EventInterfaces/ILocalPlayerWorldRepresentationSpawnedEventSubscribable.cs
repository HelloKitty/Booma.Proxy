using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public interface ILocalPlayerWorldRepresentationSpawnedEventSubscribable
	{
		event EventHandler<LocalPlayerWorldObjectSpawnedEventArgs> OnLocalPlayerWorldObjectCreated;
	}

	public sealed class LocalPlayerWorldObjectSpawnedEventArgs : EventArgs, IEntityIdentifable
	{
		public int EntityGuid { get; }

		public GameObject WorldObject { get; }

		/// <inheritdoc />
		public LocalPlayerWorldObjectSpawnedEventArgs(int entityGuid, [NotNull] GameObject worldObject)
		{
			//TODO: Check entity guid validity

			EntityGuid = entityGuid;
			WorldObject = worldObject ?? throw new ArgumentNullException(nameof(worldObject));
		}
	}
}
