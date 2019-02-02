using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardians;
using UnityEngine;
using UnityEngine.UI;

namespace Booma.Proxy
{
	/// <summary>
	/// The implementation of adaptation between <see cref="Button"/> and <see cref="IUIButton"/>.
	/// </summary>
	public sealed class UnityButtonUIButtonAdapterImplementation : BaseUnityUIAdapterImplementation, IUIButton
	{
		private UnityEngine.UI.Button UnityButton { get; }

		/// <inheritdoc />
		protected override string LoggableComponentName => UnityButton.gameObject.name;

		/// <inheritdoc />
		public UnityButtonUIButtonAdapterImplementation([NotNull] Button unityButton)
		{
			UnityButton = unityButton ?? throw new ArgumentNullException(nameof(unityButton));
		}

		/// <inheritdoc />
		public void AddOnClickListener(Action action)
		{
			if(action == null) throw new ArgumentNullException(nameof(action));

			UnityButton.onClick.AddListener(() => action());
		}

		/// <inheritdoc />
		public void AddOnClickListenerAsync(Func<Task> action)
		{
			if(action == null) throw new ArgumentNullException(nameof(action));

			//Supporting async button events from the Unity engine button is abit complex.
			AddOnClickListener(() => AsyncUnityEngineButtonCallbackHandler(action));
		}

		/// <inheritdoc />
		public bool IsInteractable
		{
			get => UnityButton.interactable;
			set => UnityButton.interactable = value;
		}

		private void AsyncUnityEngineButtonCallbackHandler(Func<Task> action)
		{
			if(action == null) throw new ArgumentNullException(nameof(action));

			//When this is called, the button has been clicked and we need async button handling.
			//This will call the async Method, get the task and create a coroutine that awaits it (for exception handling purposes)
			UnityButton.StartCoroutine(AsyncCallbackHandler(action()));
		}
	}
}
