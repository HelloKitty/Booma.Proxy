using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardians;
using UnityEngine;

namespace Booma.Proxy
{
	[AdditionalRegisterationAs(typeof(IFactoryCreatable<GameObject, LocalPlayerWorldRepresentationCreationContext>))]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class LobbyLocalPlayerWorldObjectFactory : IFactoryCreatable<GameObject, LocalPlayerWorldRepresentationCreationContext>, IGameInitializable
	{
		private IRoomCollection Rooms { get; }

		private IWorldObjectToEntityMappable WorldPlayerMap { get; }

		private IEntityGuidMappable<GameObject> EntityGuidToGameObjectMappable { get; }

		private INetworkPlayerPrefabProvider PrefabProvider { get; }

		/// <inheritdoc />
		public LobbyLocalPlayerWorldObjectFactory([NotNull] IRoomCollection rooms, [NotNull] IWorldObjectToEntityMappable worldPlayerMap, [NotNull] INetworkPlayerPrefabProvider prefabProvider, [NotNull] IEntityGuidMappable<GameObject> entityGuidToGameObjectMappable)
		{
			Rooms = rooms ?? throw new ArgumentNullException(nameof(rooms));
			WorldPlayerMap = worldPlayerMap ?? throw new ArgumentNullException(nameof(worldPlayerMap));
			PrefabProvider = prefabProvider ?? throw new ArgumentNullException(nameof(prefabProvider));
			EntityGuidToGameObjectMappable = entityGuidToGameObjectMappable ?? throw new ArgumentNullException(nameof(entityGuidToGameObjectMappable));
		}

		/// <inheritdoc />
		public GameObject Create(LocalPlayerWorldRepresentationCreationContext context)
		{
			GameObject player = GameObject.Instantiate(PrefabProvider.LocalPlayerPrefab, context.SpawnData.AssociatedObject.Position, context.SpawnData.AssociatedObject.Rotation);

			//This makes it so things can go from GameObject (root) to entity guid.
			WorldPlayerMap.Add(player, context.SpawnData.EntityGuid);
			//This allows for the reverse
			EntityGuidToGameObjectMappable.Add(context.SpawnData.EntityGuid, player);

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
