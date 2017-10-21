using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Makes a component a reciever of <see cref="INetworkPlayer"/>s entering/exiting
	/// its physica volume events.
	/// </summary>
	[Injectee]
	public abstract class NetworkPlayerTrigger : SerializedMonoBehaviour
	{
		[Inject]
		private INetworkPlayerCollection PlayerCollection { get; }

		/// <summary>
		/// Called when a player enters a trigger volume.
		/// </summary>
		/// <param name="player">The player entering.</param>
		public abstract void OnPlayerEnter(INetworkPlayer player);

		/// <summary>
		/// Called when a player leaves a trigger volume.
		/// </summary>
		/// <param name="player">The player exiting.</param>
		public abstract void OnPlayerExit(INetworkPlayer player);

		private void OnTriggerEnter(Collider other)
		{
			IEntityIdentity identity = other.GetComponent<IEntityIdentity>();

			if(!PlayerCollection.ContainsId(identity.EntityId))
				throw new InvalidOperationException($"Unknown Entity: {identity.EntityId} entered trigger.");

			OnPlayerEnter(PlayerCollection[identity.EntityId]);
		}

		private void OnTriggerExit(Collider other)
		{
			IEntityIdentity identity = other.GetComponent<IEntityIdentity>();

			if(!PlayerCollection.ContainsId(identity.EntityId))
				return;

			OnPlayerExit(PlayerCollection[identity.EntityId]);
		}
	}
}
