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
	[Injectee]
	public sealed class GameNetworkItemFactory : SerializedMonoBehaviour, INetworkEntityFactory<INetworkItem>
	{
		//TODO: We need a better system for creating items of different types
		[Required]
		[SerializeField]
		private GameObject TemporaryItemPrefab;

		[Inject]
		private INetworkEntityRegistery<INetworkItem> ItemWorldRegistery { get; }

		/// <summary>
		/// Sceneject service that lets us create <see cref="GameObject"/>s that
		/// have themselves dependencies.
		/// </summary>
		[Inject]
		public IGameObjectFactory GameObjectFactory { get; }

		/// <inheritdoc />
		public INetworkItem CreateEntity(int id)
		{
			return CreateEntity(id, Vector3.zero, Quaternion.identity);
		}

		/// <inheritdoc />
		public INetworkItem CreateEntity(int id, Vector3 position, Quaternion rotation)
		{
			if(id < 0) throw new ArgumentOutOfRangeException(nameof(id), $"The {nameof(id)} for the item must be above 0.");

			IGameObjectContextualBuilder builder = GameObjectFactory.CreateBuilder();

			//TODO: Create entity identify for items
			//Create the player GameObject and provide an identiy object to it.
			INetworkItem item = builder
				.With(Service<IEntityIdentity>.As(new EntityIdentity(id, EntityType.Item)))
				.Create(TemporaryItemPrefab, position, rotation)
				.GetComponent(typeof(INetworkItem)) as INetworkItem;

			if(item == null)
				throw new InvalidOperationException($"Failed to create a {nameof(INetworkItem)}. The prefab was missing the component.");

			//Now we must register it in the player registry
			ItemWorldRegistery.AddEntity(id, item);

			return item;
		}
	}
}
