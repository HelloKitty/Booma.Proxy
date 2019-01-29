using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.UI;
using Unitysync.Async;

namespace Guardians
{
	public sealed class UnityToggleUIToggleAdapter : BaseUnityUIAdapter<Toggle, IUIToggle>, IUIToggle
	{
		/// <inheritdoc />
		public void AddOnToggleChangedListener(Action<bool> action)
		{
			UnityUIObject.onValueChanged.AddListener(b => action(b));
		}

		/// <inheritdoc />
		public void AddOnToggleChangedListenerAsync(Func<bool, Task> action)
		{
			if(action == null) throw new ArgumentNullException(nameof(action));

			UnityUIObject.onValueChanged.AddListener(value =>
			{
				StartCoroutine(this.AsyncCallbackHandler(action(value)));
			});
		}

		/// <inheritdoc />
		public bool IsInteractable
		{
			get => UnityUIObject.interactable;
			set => UnityUIObject.interactable = value;
		}
	}
}
