using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardians;
using UnityEngine;

namespace Booma.Proxy
{
	[AdditionalRegisterationAs(typeof(IFactoryCreatable<GameObject, RemotePlayerWorldRepresentationCreationContext>))]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	public sealed class LobbyRemotePlayerWorldObjectFactory : IFactoryCreatable<GameObject, RemotePlayerWorldRepresentationCreationContext>, IGameInitializable
	{
		private IRoomCollection Rooms { get; }

		private IWorldObjectToEntityMappable WorldPlayerMap { get; }

		private IEntityGuidMappable<GameObject> EntityGuidToGameObjectMappable { get; }

		private INetworkPlayerPrefabProvider PrefabProvider { get; }

		private IReadonlyEntityGuidMappable<WorldTransform> EntityWorldTransformMappable { get; }

		private IEntityGuidMappable<MovementManager> MovementManagerMappable { get; }

		/// <inheritdoc />
		public LobbyRemotePlayerWorldObjectFactory([NotNull] IRoomCollection rooms, 
			[NotNull] IWorldObjectToEntityMappable worldPlayerMap, 
			[NotNull] INetworkPlayerPrefabProvider prefabProvider, 
			[NotNull] IEntityGuidMappable<GameObject> entityGuidToGameObjectMappable, 
			[NotNull] IReadonlyEntityGuidMappable<WorldTransform> entityWorldTransformMappable,
			[NotNull] IEntityGuidMappable<MovementManager> movementManagerMappable)
		{
			Rooms = rooms ?? throw new ArgumentNullException(nameof(rooms));
			WorldPlayerMap = worldPlayerMap ?? throw new ArgumentNullException(nameof(worldPlayerMap));
			PrefabProvider = prefabProvider ?? throw new ArgumentNullException(nameof(prefabProvider));
			EntityGuidToGameObjectMappable = entityGuidToGameObjectMappable ?? throw new ArgumentNullException(nameof(entityGuidToGameObjectMappable));
			EntityWorldTransformMappable = entityWorldTransformMappable ?? throw new ArgumentNullException(nameof(entityWorldTransformMappable));
			MovementManagerMappable = movementManagerMappable ?? throw new ArgumentNullException(nameof(movementManagerMappable));
		}

		/// <inheritdoc />
		public GameObject Create(RemotePlayerWorldRepresentationCreationContext context)
		{
			//TODO: Save to assume non-cheaters have sent the required position data.
			WorldTransform transform = EntityWorldTransformMappable[context.EntityGuid];

			GameObject player = GameObject.Instantiate(PrefabProvider.RemotePlayerPrefab, transform.Position, transform.Rotation);

			//This makes it so things can go from GameObject (root) to entity guid.
			WorldPlayerMap.Add(player, context.EntityGuid);
			//This allows for the reverse
			EntityGuidToGameObjectMappable.Add(context.EntityGuid, player);

			//TODO: Maybe make movementmanager in a factory?
			//TODO: This is kinda a hack to down cast. We don't have another way right now.
			//Remote players also get a default movement manager
			MovementManagerMappable.Add(context.EntityGuid, new MovementManager((IReadonlyEntityGuidMappable<GameObject>)EntityGuidToGameObjectMappable));

			//TODO: How should we handle rooms?
			//Rooms.DefaultRoom.TryEnter(player);
			return player;
		}

		//TODO: This is kinda a hack to get into the scene
		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
