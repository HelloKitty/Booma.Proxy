using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Booma.Proxy
{
	/// <summary>
	/// UI Controller for ship list.
	/// </summary>
	[Injectee]
	public sealed class ShipListUIController : SerializedMonoBehaviour, IShipListingRegisterable
	{
		/// <summary>
		/// Prefab for the ship entry.
		/// </summary>
		[Required]
		[SerializeField]
		private GameObject ShipEntryPrefab;

		/// <summary>
		/// The root menu/panel for the ship list.
		/// </summary>
		[Required]
		[SerializeField]
		private GameObject ShipMenuPanelObject;

		[Tooltip("The initial offset from the top of the panel")]
		[SerializeField]
		private int InitialYOffset;

		[Tooltip("The offset per shiplisting to use.")]
		[SerializeField]
		private int OffsetPerListing;

		[Inject]
		private IClientPayloadSendService<PSOBBLoginPacketPayloadClient> SendService { get; }

		public void RegisterShip(ShipListing model)
		{
			if(model == null) throw new ArgumentNullException(nameof(model));

			//Create the prefab at the required offset
			GameObject shipEntry = GameObject.Instantiate(ShipEntryPrefab, new Vector3(0, InitialYOffset + (OffsetPerListing * model.Selection.ItemId), 0), Quaternion.identity);

			//Rig up the button to dispatch to this controller's press handler.
			Button button = shipEntry.GetComponent<UnityEngine.UI.Button>();
			Text text = shipEntry.GetComponent<UnityEngine.UI.Text>();

			if(button == null)
				throw new InvalidOperationException($"The {shipEntry.name} {nameof(ShipEntryPrefab)} contains no button.");

			text.text = shipEntry.name.Replace("Destiny", "[redacted]");

			button.onClick.AddListener(() =>
			{
				ClickMenuItem(model.Selection.MenuId, model.Selection.ItemId);
			});
		}

		public void ClickMenuItem(uint menuId, uint itemId)
		{
			//Disable the ship panel
			ShipMenuPanelObject.SetActive(false);

			//Send the menu selection request. The server will redirect us if it's success.
			SendService.SendMessage(new LoginMenuSelectionRequestPayload(new MenuItemIdentifier(menuId, itemId)));
		}
	}
}
