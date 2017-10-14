using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using SceneJect.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Booma.Proxy
{
	[Injectee]
	public abstract class BaseMenuUIController : SerializedMonoBehaviour
	{
		/// <summary>
		/// Prefab for the menu entry.
		/// </summary>
		[Required]
		[SerializeField]
		private GameObject _MenuEntryPrefab;

		protected GameObject MenuEntryPrefab => _MenuEntryPrefab;

		/// <summary>
		/// The root menu/panel for the list.
		/// </summary>
		[Required]
		[SerializeField]
		private GameObject _MenuPanelObject;

		protected GameObject MenuPanelObject => _MenuPanelObject;

		[Tooltip("The initial offset from the top of the panel")]
		[SerializeField]
		private int _InitialYOffset;

		protected int InitialYOffset => _InitialYOffset;

		[Tooltip("The offset per mmenu listing to use.")]
		[SerializeField]
		private int _OffsetPerListing;

		protected int OffsetPerListing => _OffsetPerListing;

		[Inject]
		protected IClientPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		[Inject]
		protected ILog Logger { get; }

		public void ClickMenuItem(uint menuId, uint itemId)
		{
			//Disable the ship panel
			MenuPanelObject.SetActive(false);

			//Send the menu selection request. The server will redirect us if it's success.
			SendService.SendMessage(new SharedMenuSelectionRequestPayload(new MenuItemIdentifier(menuId, itemId)));
		}

		/// <summary>
		/// Adds the networked button click message
		/// to the provided <see cref="Button"/>'s onClick event.
		/// </summary>
		/// <param name="button">The button.</param>
		protected void AddNetworkClickToButtonListener(Button button, uint menuId, uint itemId)
		{
			if(button == null) throw new ArgumentNullException(nameof(button));

			button.onClick.AddListener(() =>
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Networked Button Click: {button.name} MenuId: {menuId} ItemId: {itemId}.");

				ClickMenuItem(menuId, itemId);
			});
		}
	}
}
