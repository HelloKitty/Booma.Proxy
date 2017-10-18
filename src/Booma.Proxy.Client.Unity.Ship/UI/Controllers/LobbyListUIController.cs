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
	public sealed class LobbyListUIController : MonoBehaviour
	{
		[Required]
		[SerializeField]
		private GameObject RootLobbySelectPanel;

		[Inject]
		private ICharacterSlotSelectedModel SlotModel { get; }

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
	}
}
