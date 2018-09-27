using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class PlayableLevelStaticComponentContainer : SerializedMonoBehaviour
	{
		/// <summary>
		/// Serialized pre-computed field of all monobehaviors
		/// that will require injection at scene load time.
		/// </summary>
		[SerializeField]
		private MonoBehaviour[] Behaviors;

		void OnEnable()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		void OnDisable()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if(GlobalGameServices.InjectionService is null)
				throw new InvalidOperationException($"{nameof(GlobalGameServices.InjectionService)} in {nameof(GlobalGameServices)} is null.");

			//Just help inject the services into the referenced behaviors.
			GlobalGameServices.InjectionService.InjectDependencies(Behaviors);
		}

		[Button]
		void CollectStaticInjectableMonoBehaviors()
		{
			var injecteeLocator = new InjecteeLocator<MonoBehaviour>();

			Behaviors = injecteeLocator.ToArray();
		}
	}
}
