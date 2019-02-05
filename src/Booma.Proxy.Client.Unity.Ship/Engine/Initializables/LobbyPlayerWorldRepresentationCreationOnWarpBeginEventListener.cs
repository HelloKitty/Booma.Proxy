using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Guardians;
using UnityEngine;

namespace Booma.Proxy
{
	[AdditionalRegisterationAs(typeof(ILocalPlayerWorldRepresentationSpawnedEventSubscribable))]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	public sealed class LobbyPlayerWorldRepresentationCreationOnWarpBeginEventListener : BaseSingleEventListenerInitializable<IWarpBeginEventSubscribable>, ILocalPlayerWorldRepresentationSpawnedEventSubscribable
	{
		private IFactoryCreatable<GameObject, LocalPlayerWorldRepresentationCreationContext> LocalPlayerWorldRepresentationFactory { get; }

		private ISpawnPointStrategy SpawnStrategy { get; }

		private ICharacterSlotSelectedModel SlotIndex { get; }

		private ILog Logger { get; }

		/// <inheritdoc />
		public event EventHandler<LocalPlayerWorldObjectSpawnedEventArgs> OnLocalPlayerWorldObjectCreated;

		/// <inheritdoc />
		public LobbyPlayerWorldRepresentationCreationOnWarpBeginEventListener(IWarpBeginEventSubscribable subscriptionService, [NotNull] IFactoryCreatable<GameObject, LocalPlayerWorldRepresentationCreationContext> localPlayerWorldRepresentationFactory, [NotNull] ISpawnPointStrategy spawnStrategy, [NotNull] ICharacterSlotSelectedModel slotIndex, [NotNull] ILog logger) 
			: base(subscriptionService)
		{
			LocalPlayerWorldRepresentationFactory = localPlayerWorldRepresentationFactory ?? throw new ArgumentNullException(nameof(localPlayerWorldRepresentationFactory));
			SpawnStrategy = spawnStrategy ?? throw new ArgumentNullException(nameof(spawnStrategy));
			SlotIndex = slotIndex ?? throw new ArgumentNullException(nameof(slotIndex));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, EventArgs args)
		{
			//This just spawns the player by grabbing a spawn point
			//and passing it to the local player factory.
			Transform spawnPoint = SpawnStrategy.GetSpawnpoint();

			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, SlotIndex.SlotSelected);

			GameObject gameObject = LocalPlayerWorldRepresentationFactory.Create(new LocalPlayerWorldRepresentationCreationContext(entityGuid, spawnPoint));

			if(Logger.IsInfoEnabled)
				Logger.Info($"Created LocalPlayer's World Object. Dispatching {nameof(OnLocalPlayerWorldObjectCreated)}");


			OnLocalPlayerWorldObjectCreated?.Invoke(this, new LocalPlayerWorldObjectSpawnedEventArgs(entityGuid, gameObject));
		}
	}
}
