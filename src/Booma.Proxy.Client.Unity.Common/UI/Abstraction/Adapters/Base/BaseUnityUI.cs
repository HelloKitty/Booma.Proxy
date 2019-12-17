using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardians;
using UnityEngine;
using Unitysync.Async;

namespace Booma.Proxy
{
	public abstract class BaseUnityUI<TAdaptedToType> : MonoBehaviour, IUIAdapterRegisterable
	{
		[Tooltip("Used to determine wiring for UI dependencies.")]
		[SerializeField]
		private UnityUIRegisterationKey _RegisterationKey;

		/// <summary>
		/// The registeration key for the adapted UI element.
		/// </summary>
		public UnityUIRegisterationKey RegisterationKey => _RegisterationKey;

		/// <inheritdoc />
		public Type UISerivdeType => typeof(TAdaptedToType);

		//TODO: Eventually we need to refactor this away.
		/// <summary>
		/// Can be called as a <see cref="StartCoroutine"/>
		/// to track the result, and dispatch the exception/logging for, async tasks.
		/// </summary>
		/// <param name="task">The task to await.</param>
		/// <returns></returns>
		protected IEnumerator AsyncCallbackHandler(Task task)
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
	}
}
