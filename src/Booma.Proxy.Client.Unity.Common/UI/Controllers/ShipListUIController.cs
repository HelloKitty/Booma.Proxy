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
	public sealed class ShipListUIController : BaseMenuUIController, IMenuListingRegisterable
	{
		private int ServerCount = 0;

		public void RegisterMenuItem(MenuListing model)
		{
			if(model == null) throw new ArgumentNullException(nameof(model));

			//TODO: Is there a better way to do this?
			//Create the prefab at the required offset
			GameObject shipEntry = GameObject.Instantiate(MenuEntryPrefab);
			shipEntry.transform.parent = MenuPanelObject.transform;
			shipEntry.GetComponent<RectTransform>().localPosition = new Vector3(0, InitialYOffset + (OffsetPerListing * ServerCount++), 0);

			//TODO: Is there a better way to do this?
			//Rig up the button to dispatch to this controller's press handler.
			Button button = shipEntry.GetComponent<UnityEngine.UI.Button>();
			Text text = shipEntry.GetComponent<UnityEngine.UI.Text>();

			text.text = model.ItemName;

			//Register the network callback for clicking the button
			AddNetworkClickToButtonListener(button, model.Selection.MenuId, model.Selection.ItemId);
		}
	}
}
