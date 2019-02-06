using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using Nito.AsyncEx;
using UnityEngine;

namespace Booma.Proxy
{
	//This current design could put the movement a frame or two behind the actual local movement
	//probably doesn't matter for PSO though.
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class NetworkMovementSendMovementChangeEventListener : BaseSingleEventListenerInitializable<IMovementInputChangedEventSubscribable, MovementInputChangedEventArgs>, IGameTickable
	{
		//Dependencies
		private IReadonlyEntityGuidMappable<GameObject> WorldObjectMap { get; }

		private ILocalPlayerNetworkMovementController LocalPlayerNetworkController { get; }

		private ICharacterSlotSelectedModel SlotModel { get; }


		//Local state
		private MovementInputChangedEventArgs CurrentMovementArgs { get; set; } = new MovementInputChangedEventArgs(0.0f, 0.0f);

		private AsyncLock SyncObj = new AsyncLock();

		//TODO: We need better state handling than a bool
		private bool isMoving = false;

		/// <inheritdoc />
		public NetworkMovementSendMovementChangeEventListener(IMovementInputChangedEventSubscribable subscriptionService, [NotNull] ILocalPlayerNetworkMovementController localPlayerNetworkController, [NotNull] IReadonlyEntityGuidMappable<GameObject> worldObjectMap, ICharacterSlotSelectedModel slotModel) 
			: base(subscriptionService)
		{
			LocalPlayerNetworkController = localPlayerNetworkController ?? throw new ArgumentNullException(nameof(localPlayerNetworkController));
			WorldObjectMap = worldObjectMap ?? throw new ArgumentNullException(nameof(worldObjectMap));
			SlotModel = slotModel;
		}

		/// <inheritdoc />
		protected override async void OnEventFired(object source, MovementInputChangedEventArgs args)
		{
			//State doesn't need to change if this is true
			//but it SHOULDN'T be true, as the event should not be
			//broadcasted
			if(CurrentMovementArgs == args)
				return;

			//It's ok to capture the sync context here, we'll need it to touch the GameObject
			//and the Transform.
			using(var l = await SyncObj.LockAsync())
			{
				//If the input says NOT MOVING but we are MOVING
				//then we need to end the movement and then send a movement finished command
				if(!args.isMoving && isMoving)
				{
					GameObject worldObject = WorldObjectMap[EntityGuid.ComputeEntityGuid(EntityType.Player, SlotModel.SlotSelected)];

					//It's important that this doesn't capture the sync context otheriwse it could block Update from finishing (causing deadlock)
					//since it needs to wait on SyncContext to be serviced by Unity3D in fixed update. It's CRITICAL that we do NOT block on the main thread
					//or such a deadlock will occur.
					await LocalPlayerNetworkController.StopMovementAsync(worldObject.transform.position, worldObject.transform.rotation)
						.ConfigureAwait(false);

					//We tell the controller to stop moving and we set our movement here to false.
					isMoving = false;
				}
				else
					isMoving = true; //it probably already is true but it doesn't hurt to set it again.

				CurrentMovementArgs = args;
			}
		}

		/// <inheritdoc />
		public void Tick()
		{
			//Don't need to do this if we aren't moving
			if(isMoving)
				using(SyncObj.Lock())
				{
					//Double check locking
					if(!isMoving)
						return;

					GameObject worldObject = WorldObjectMap[EntityGuid.ComputeEntityGuid(EntityType.Player, SlotModel.SlotSelected)];

					LocalPlayerNetworkController.UpdatedMovementLocation(worldObject.transform.position, worldObject.transform.rotation);
				}
		}
	}
}
