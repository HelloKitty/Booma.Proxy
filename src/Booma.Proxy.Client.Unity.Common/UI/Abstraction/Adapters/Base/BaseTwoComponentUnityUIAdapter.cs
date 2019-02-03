using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Guardians;
using UnityEngine;

namespace Booma.Proxy
{
	public abstract class BaseTwoComponentUnityUIAdapter<TAdaptedUnityEngineType, TAdaptedUnityEngineType2, TAdaptedToType> : BaseUnityUIAdapter<TAdaptedUnityEngineType, TAdaptedToType>
	{
		[SerializeField]
		protected TAdaptedUnityEngineType2 _UnityUIObject2;

		/// <summary>
		/// The Unity engine UI object being adapted.
		/// </summary>
		protected TAdaptedUnityEngineType2 UnityUIObject2 => _UnityUIObject2;

		//TODO: Should we put this in base UI?
		//The reason we need this feature is it's posible for
		//initialization Awake/Start functions in child types to NEVER be called
		//because they are asleep. It's important that we expose a way to run this ourselves.
		/// <summary>
		/// Indicates if the component has been initialized.
		/// </summary>
		protected bool isInitialized { get; private set; } = false; //defaults to uninit.

		private object SyncRoot { get; } = new object();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Awake()
		{
			//If not init then we should run init
			//this could be called directly by Unity
			//or it could be force called lazily by ourselves
			//meaning we must ensure it's only called once ever, and on one thread.
			if(!isInitialized)
			{
				lock(SyncRoot)
				{
					if(isInitialized)
						return;

					Initialize();
				}
			}
		}

		/// <summary>
		/// Components that inherit from <see cref="BaseUnityUIAdapterImplementation"/>.
		/// Will only ever be called once. Either early or at the last moment.
		/// </summary>
		protected abstract void Initialize();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected void EnsureInitialized()
		{
			if(!isInitialized)
				Awake();
		}
	}
}
