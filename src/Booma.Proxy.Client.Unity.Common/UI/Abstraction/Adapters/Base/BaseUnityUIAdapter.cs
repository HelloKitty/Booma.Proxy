using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Unitysync.Async;

namespace Guardians
{
	public abstract class BaseUnityUIAdapter<TAdaptedUnityEngineType, TAdaptedToType> : SerializedMonoBehaviour, IUIAdapterRegisterable
	{
		static BaseUnityUIAdapter()
		{
			if(typeof(TAdaptedToType) == typeof(TAdaptedUnityEngineType))
				throw new InvalidOperationException($"Type: BaseUnityUIAdapter<{typeof(TAdaptedUnityEngineType).Name}, {typeof(TAdaptedToType).Name}> must not have the same parameter for both generic type parameters.");

			//TODO: Check that TAdaptedUnityEngineType is in the Unity namespace.
		}

		[SerializeField]
		private TAdaptedUnityEngineType _UnityUIObject;

		/// <summary>
		/// The Unity engine UI object being adapted.
		/// </summary>
		protected TAdaptedUnityEngineType UnityUIObject => _UnityUIObject;

		[Tooltip("Used to determine wiring for UI dependencies.")]
		[SerializeField]
		private UnityUIRegisterationKey _RegisterationKey;

		/// <summary>
		/// The registeration key for the adapted UI element.
		/// </summary>
		public UnityUIRegisterationKey RegisterationKey => _RegisterationKey;

		/// <inheritdoc />
		public Type UISerivdeType => typeof(TAdaptedToType);

		[Button]
		public void TryInitializeAdaptedObject()
		{
			TAdaptedUnityEngineType obj = GetComponent<TAdaptedUnityEngineType>();

			if(obj == null)
				throw new InvalidOperationException($"Failed to find {typeof(TAdaptedUnityEngineType).Name} on GameObject: {name}.");

			if(!ValidateInitializedObject(obj))
				return;

			_UnityUIObject = obj;
		}

		/// <summary>
		/// Validates that the provided <see cref="obj"/>
		/// is valid to be initialized as the adapted engine object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>True if it's valid.</returns>
		protected virtual bool ValidateInitializedObject(TAdaptedUnityEngineType obj)
		{
			return obj != null;
		}

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
