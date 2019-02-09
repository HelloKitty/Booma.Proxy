using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	public sealed class MovementInputLocalPlayerMotorEventListener : BaseSingleEventListenerInitializable<IMovementInputChangedEventSubscribable, MovementInputChangedEventArgs>, IGameTickable
	{
		private MovementInputChangedEventArgs CurrentMovementArgs { get; set; } = new MovementInputChangedEventArgs(0,0);

		private IReadonlyEntityGuidMappable<GameObject> GuidToWorldObjectMappable { get; }

		private ICharacterSlotSelectedModel SlotModel { get; }

		public float Speed = 10.0f;

		/// <inheritdoc />
		public MovementInputLocalPlayerMotorEventListener(IMovementInputChangedEventSubscribable subscriptionService, [NotNull] IReadonlyEntityGuidMappable<GameObject> guidToWorldObjectMappable, [NotNull] ICharacterSlotSelectedModel slotModel) 
			: base(subscriptionService)
		{
			GuidToWorldObjectMappable = guidToWorldObjectMappable ?? throw new ArgumentNullException(nameof(guidToWorldObjectMappable));
			SlotModel = slotModel ?? throw new ArgumentNullException(nameof(slotModel));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, [NotNull] MovementInputChangedEventArgs args)
		{
			CurrentMovementArgs = args ?? throw new ArgumentNullException(nameof(args));
		}

		/// <inheritdoc />
		public void Tick()
		{
			float ver = CurrentMovementArgs.NewVerticalInput;
			float hor = CurrentMovementArgs.NewHorizontalInput;

			GameObject player = GuidToWorldObjectMappable[EntityGuid.ComputeEntityGuid(EntityType.Player, SlotModel.SlotSelected)];

			player.transform.position += player.transform.forward * ver * Speed * Time.deltaTime + player.transform.right * hor * Speed * Time.deltaTime;

			//TODO: Handle rotation again someday
			/*if(Input.GetKey(KeyCode.E))
				transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.y + Speed * Time.deltaTime * 10.0f, Vector3.up);
			else if(Input.GetKey(KeyCode.Q))
				transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.y + -Speed * Time.deltaTime * 10.0f, Vector3.up);*/
		}
	}
}
