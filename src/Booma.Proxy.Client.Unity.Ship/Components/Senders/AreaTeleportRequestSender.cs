using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nito.AsyncEx;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class AreaTeleportRequestSender : NetworkRequestSender
	{
		//TODO: Put this behind simplier service
		//We need this service so that we can know our own identifier
		[Inject]
		private INetworkPlayerCollection Players { get; }

		[Inject]
		private IGameObjectComponentAttachmentFactory ComponentFactory { get; }

		//TODO: Right now we're using this as a hack to delete P2, so we need to change that.
		[SerializeField]
		private UnityEvent OnTeleportToArea;

		public void TeleportToArea(int areaId)
		{
			TeleportToAreaAsync(areaId)
				.ConfigureAwait(true);
		}

		private async Task TeleportToAreaAsync(int areaId)
		{
			//Client says 0x60 0x21 when it's "I am now begining to teleport!" so other clients can ready themselves
			await this.SendService.SendMessage(new Sub60StartNewWarpCommand((byte)Players.Local.Identity.EntityId, (short)areaId).ToPayload())
				.ConfigureAwait(true);

			//Client teleporting says 0x60 0x22 as in "HEY, I am about to teleport" so other clients can prepare
			await this.SendService.SendMessage(new Sub60ClientZoneTeleportingEventCommand((byte)Players.Local.Identity.EntityId).ToPayload())
				.ConfigureAwait(true);

			//TODO: client sometimes sends 75 when it's teleporting but
			//don't know what that is yet.

			//We don't send 0x60 0x23 right now, that should be sent in the next scene
			//when we're done loading and essentially warping
			//So we create a special object in the scene that will handle this.

			//TODO: Extract this into a factory
			//We create a new gameobject and attach the component required
			GameObject o = new GameObject();
			ComponentFactory.AddTo<AreaTeleportTransferCompletion>(o);

			OnTeleportToArea?.Invoke();

			//TODO: Handle loading better, we need to refactor this to actually support all scenes/levels.
			//just load the scene, we use additive because it's actually easier to manage
			//the complexities of dealing with an additively loaded scene, and injecting the dependencies that it requires
			//then bootstrapping a scene and then loading into it carrying all state/data and etc.
			SceneManager.LoadScene("forest_test", LoadSceneMode.Additive);
		}
	}
}
