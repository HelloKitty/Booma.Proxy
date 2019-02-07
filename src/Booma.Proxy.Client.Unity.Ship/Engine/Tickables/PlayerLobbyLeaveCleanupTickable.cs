using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class PlayerLobbyLeaveCleanupTickable : BaseSingleEventListenerInitializable<IRemotePlayerLeaveLobbyEventSubscribable, RemotePlayerLeaveLobbyEventArgs>
	{
		private IEntityCollectionRemovable[] Removables { get; }

		private IEntityGuidMappable<GameObject> WorldObjectMappable { get; }

		/// <inheritdoc />
		public PlayerLobbyLeaveCleanupTickable(IRemotePlayerLeaveLobbyEventSubscribable subscriptionService, [NotNull] IEnumerable<IEntityCollectionRemovable> removables, [NotNull] IEntityGuidMappable<GameObject> worldObjectMappable) 
			: base(subscriptionService)
		{
			if(removables == null) throw new ArgumentNullException(nameof(removables));

			WorldObjectMappable = worldObjectMappable ?? throw new ArgumentNullException(nameof(worldObjectMappable));
			Removables = removables.ToArray();
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, RemotePlayerLeaveLobbyEventArgs args)
		{
			//We should destroy the object first
			if(WorldObjectMappable.ContainsKey(args.EntityGuid))
			{
				//It is possible that the world representation was NOT spawned yet.
				//It is very possible. They could almost join and then disconnect.
				//Or spoof some packets causing an exception and we won't end up cleaning up
				//the below critical entity associated data.
				GameObject.Destroy(WorldObjectMappable[args.EntityGuid]);
			}

			//We have to handle in place right now, otherwise someone could spawn
			//with the same id before we client up.
			for(int i = 0; i < Removables.Length; i++)
			{
				//We just remove all entity data related directly to them.
				Removables[i].RemoveEntityEntry(args.EntityGuid);
			}
		}
	}
}
