using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using Unitysync.Async;

namespace Guardians
{
	public sealed class UnityButtonUIButtonAdapter : BaseUnityUIAdapter<Button, IUIButton>, IUIButton
	{
		/// <inheritdoc />
		public void AddOnClickListener(Action action)
		{
			if(action == null) throw new ArgumentNullException(nameof(action));

			UnityUIObject.onClick.AddListener(() => action());
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
			get => UnityUIObject.interactable;
			set => UnityUIObject.interactable = value;
		}

		private void AsyncUnityEngineButtonCallbackHandler(Func<Task> action)
		{
			if(action == null) throw new ArgumentNullException(nameof(action));

			//When this is called, the button has been clicked and we need async button handling.
			//This will call the async Method, get the task and create a coroutine that awaits it (for exception handling purposes)
			StartCoroutine(AsyncButtonEventHandler(action()));
		}

		private IEnumerator AsyncButtonEventHandler(Task task)
		{
			if(task == null) throw new ArgumentNullException(nameof(task));

			//This will wait until the task is complete
			yield return new WaitForFuture(task);

			if(task.IsFaulted)
			{
				StringBuilder builder = new StringBuilder(200);

				if(task.Exception != null && task.Exception.InnerExceptions != null)
					foreach(Exception inner in task.Exception?.InnerExceptions)
					{
						builder.Append($"\nMessage: {inner.Message}\nStack: {inner.StackTrace}");
					}

				UnityEngine.Debug.LogError($"Encounter exception from Button: {name} OnClickAsync: {builder.ToString()}");
			}

			//We don't need to do anything, task succeeded and is finished.
		}

		//TODO: Have UnitySync.Async support exception forwarding.
		private void DummyMethod()
		{

		}
	}
}
