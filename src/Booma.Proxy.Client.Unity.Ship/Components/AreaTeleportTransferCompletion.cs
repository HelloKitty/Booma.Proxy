using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	/// <summary>
	/// Component responsible for finishing the area teleport process
	/// in the next scene that the local user decided to teleport to.
	/// </summary>
	[Injectee]
	public sealed class AreaTeleportTransferCompletion : NetworkRequestSender
	{
		[Inject]
		private INetworkPlayerCollection Players { get; }

		[Inject]
		private IUnitScalerStrategy ScalerService { get; }

		void Awake()
		{
			//We need to say we shouldn't be destroyed, and register a callback on scene load to finish teleporting.
			DontDestroyOnLoad(this.gameObject);
			SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
		}

		private void SceneManagerOnSceneLoaded(Scene sceneLoaded, LoadSceneMode loadMode)
		{
			Debug.Log("On level loaded fired");
			SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;

			//Alert everyone to where we are going to spawn
			this.SendService.SendMessage(new Sub60TeleportToPositionCommand(Players.Local.Identity.EntityId, ScalerService.UnScale(Players.Local.Transform.Position).ToNetworkVector3()).ToPayload());

			//TODO: Don't hardcode zone!
			this.SendService.SendMessage(new Sub60WarpToNewAreaCommand(Players.Local.Identity.EntityId, 1).ToPayload());

			//Alert that we have loaded the map
			this.SendService.SendMessage(new Sub60FinishedMapLoadCommand(Players.Local.Identity.EntityId).ToPayload());

			//No matter what the scene was, we should finish the teleport/warp process.
			//Client sends 0x60 0x23 right now, saying that it is done warping/loading
			this.SendService.SendMessage(new Sub60FinishedWarpingBurstingCommand((byte)Players.Local.Identity.EntityId).ToPayload());

			//TODO: We need to set the local player at the new maps spawnpoint.
			//Players.Local.Transform.Position

			//This should not persist, it should be removed
			Destroy(this.gameObject);
		}
	}
}
