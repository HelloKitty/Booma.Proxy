using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using SceneJect.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class LobbyListUIController : MonoBehaviour
	{
		[Serializable]
		public class OnLoadNewLobbyEvent : UnityEvent<int> { }

		[Required]
		[SerializeField]
		private GameObject RootLobbySelectPanel;

		[Inject]
		private ICharacterSlotSelectedModel SlotModel { get; }

		[Inject]
		private ILog Logger { get; }

		/// <summary>
		/// Event dispatched before loading the new lobby scene.
		/// </summary>
		[SerializeField]
		private UnityEvent OnBeforeLoadNewLobby;

		[SerializeField]
		private OnLoadNewLobbyEvent OnLoadNewLobby;

		public void OnTriggerEnter(Collider other)
		{
			if(!isLocalPlayer(other.gameObject))
				return;
			
			//Enable the lobby selection
			RootLobbySelectPanel.SetActive(true);
		}

		public void OnTriggerExit(Collider other)
		{
			if(!isLocalPlayer(other.gameObject))
				return;

			//Enable the lobby selection
			RootLobbySelectPanel.SetActive(false);
		}

		private bool isLocalPlayer(GameObject obj)
		{
			//We need to know who is entering
			IEntityIdentity identity = obj.GetComponent<IEntityIdentity>();

			//If it's not the local player's id we should not do anything
			if(identity == null || SlotModel.SlotSelected != identity.EntityId)
				return false;

			return true;
		}

		/// <summary>
		/// Called to load a new lobby.
		/// </summary>
		/// <param name="id">The id of the scene to load.</param>
		public void LoadNewLobby(int id)
		{
			OnBeforeLoadNewLobby?.Invoke();

			if(OnLoadNewLobby == null && Logger.IsWarnEnabled)
				Logger.Warn($"There was no lobby loading event setup.");

			OnLoadNewLobby?.Invoke(id);
		}
	}
}
