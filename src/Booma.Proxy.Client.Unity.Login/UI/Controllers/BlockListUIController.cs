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
	/// UI Controller for block list.
	/// </summary>
	[Injectee]
	public sealed class BlockListUIController : BaseMenuUIController, IMenuListingRegisterable
	{
		private int BlockCount = 0;

		public void RegisterMenuItem(MenuListing model)
		{
			if(model == null) throw new ArgumentNullException(nameof(model));

			//TODO: Is there a better way to do this?
			//Create the prefab at the required offset
			GameObject blockEntry = GameObject.Instantiate(MenuEntryPrefab);
			blockEntry.transform.parent = MenuPanelObject.transform;
			blockEntry.GetComponent<RectTransform>().localPosition = new Vector3(0, InitialYOffset + (OffsetPerListing * BlockCount++), 0);

			//TODO: Is there a better way to do this?
			//Rig up the button to dispatch to this controller's press handler.
			Button button = blockEntry.GetComponent<UnityEngine.UI.Button>();
			Text text = blockEntry.GetComponent<UnityEngine.UI.Text>();

			text.text = model.ItemName;

			//Register the network callback for clicking the button
			AddNetworkClickToButtonListener(button, model.Selection.MenuId, model.Selection.ItemId);
		}
	}
}
