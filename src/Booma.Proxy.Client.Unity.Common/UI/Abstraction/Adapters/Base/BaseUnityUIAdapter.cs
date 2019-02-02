using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Booma.Proxy;
using Sirenix.OdinInspector;
using UnityEngine;
using Unitysync.Async;

namespace Guardians
{
	public abstract class BaseUnityUIAdapter<TAdaptedUnityEngineType, TAdaptedToType> : BaseUnityUI<TAdaptedToType>
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
	}
}
