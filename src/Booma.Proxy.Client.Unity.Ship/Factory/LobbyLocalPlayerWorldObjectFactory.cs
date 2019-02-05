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

		private INetworkPlayerPrefabProvider PrefabProvider { get; }

		/// <inheritdoc />
		public LobbyLocalPlayerWorldObjectFactory([NotNull] IRoomCollection rooms, [NotNull] IWorldObjectToEntityMappable worldPlayerMap, [NotNull] INetworkPlayerPrefabProvider prefabProvider)
		{
			Rooms = rooms ?? throw new ArgumentNullException(nameof(rooms));
			WorldPlayerMap = worldPlayerMap ?? throw new ArgumentNullException(nameof(worldPlayerMap));
			PrefabProvider = prefabProvider ?? throw new ArgumentNullException(nameof(prefabProvider));
		}

		/// <inheritdoc />
		public GameObject Create(LocalPlayerWorldRepresentationCreationContext context)
		{
			GameObject player = GameObject.Instantiate(PrefabProvider.LocalPlayerPrefab, context.SpawnData.AssociatedObject.Position, context.SpawnData.AssociatedObject.Rotation);

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
