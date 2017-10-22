using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SceneJect.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class LobbyNetworkPlayerFactory : SerializedMonoBehaviour, INetworkPlayerFactory
	{
		[Required]
		[SerializeField]
		private GameObject NetworkPlayerLobbyPrefab;

		[Required]
		[SerializeField]
		private GameObject LocalPlayerLobbyPrefab;

		/// <summary>
		/// Sceneject service that lets us create <see cref="GameObject"/>s that
		/// have themselves dependencies.
		/// </summary>
		[Inject]
		public IGameObjectFactory GameObjectFactory { get; }

		[Inject]
		private INetworkPlayerRegistery PlayerRegistery { get; }

		[Inject]
		private IRoomCollection Rooms { get; }

		/// <summary>
		/// Strategy for selecting where to spawn. Some floors
		/// can have multiple viable spawn points. Such as soccer lobbies.
		/// Same with places like Forest and such/
		/// </summary>
		[PropertyTooltip("Strategy for picking a spawn point.")]
		[SerializeField]
		private ISpawnPointStrategy SpawnPointStrategy;

		/// <inheritdoc />
		public INetworkPlayer CreatePlayer(int id, Vector3 position, Quaternion rotation)
		{
			if(id < 0 || id > byte.MaxValue) throw new ArgumentOutOfRangeException(nameof(id), $"The {nameof(id)} for the player must not exceed 255 or be below 0.");

			IGameObjectContextualBuilder builder = GameObjectFactory.CreateBuilder();

			//Create the player GameObject and provide an identiy object to it.
			INetworkPlayer player = builder
				.With(Service<IEntityIdentity>.As(new PlayerEntityIdentity((byte)id)))
				.Create(NetworkPlayerLobbyPrefab, position, rotation)
				.GetComponent(typeof(INetworkPlayer)) as INetworkPlayer;

			if(player == null)
				throw new InvalidOperationException($"Failed to create a {nameof(INetworkPlayer)}. The prefab was missing the component.");

			//Now we must register it in the player registry
			PlayerRegistery.AddPlayer(id, player);
			Rooms.DefaultRoom.TryEnter(player);

			return player;
		}

		/// <inheritdoc />
		public INetworkPlayer CreatePlayer(int id)
		{
			Transform spawnpoint = SpawnPointStrategy.GetSpawnpoint();

			if(spawnpoint == null)
				throw new InvalidOperationException($"The {this.GetType().Name} tried to load a spawnpoint from {nameof(SpawnPointStrategy)} but the point was null.");

			return CreatePlayer(id, spawnpoint.position, spawnpoint.rotation);
		}

		/// <inheritdoc />
		public INetworkPlayer CreateLocalPlayer()
		{
			Transform spawnpoint = SpawnPointStrategy.GetSpawnpoint();

			if(spawnpoint == null)
				throw new InvalidOperationException($"The {this.GetType().Name} tried to load a spawnpoint from {nameof(SpawnPointStrategy)} but the point was null.");

			IGameObjectContextualBuilder builder = GameObjectFactory.CreateBuilder();

			//TODO: Implement local player spawning.
			//Create the player GameObject and provide an identiy object to it.
			INetworkPlayer player = builder
				.With(Service<IEntityIdentity>.As(context => new LocalPlayerEntityIdentity(context.Resolve<ICharacterSlotSelectedModel>())))
				.Create(LocalPlayerLobbyPrefab, spawnpoint.position, spawnpoint.rotation)
				.GetComponent(typeof(INetworkPlayer)) as INetworkPlayer;

			if(player == null)
				throw new InvalidOperationException($"Failed to create a {nameof(INetworkPlayer)}. The prefab was missing the component.");

			if(player.Identity == null)
				throw new InvalidOperationException($"The identity was null.");

			//Now we must register it in the player registry
			PlayerRegistery.AddPlayer(player.Identity.EntityId, player);
			Rooms.DefaultRoom.TryEnter(player);

			return player;
		}
	}
}
