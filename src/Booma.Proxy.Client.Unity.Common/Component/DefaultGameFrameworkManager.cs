using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Default game framework manager that calls init
	/// and ticks tickables.
	/// </summary>
	[Injectee]
	public sealed class DefaultGameFrameworkManager : MonoBehaviour
	{
		/// <summary>
		/// Collection of all game initializables.
		/// </summary>
		[Inject]
		private IEnumerable<IGameInitializable> Initializables { get; }

		[Inject]
		private IEnumerable<IGameTickable> Tickables { get; }

		[ReadOnly]
		[ShowInInspector]
		private bool isInitializationFinished = false;

		private async Task Start()
		{
			//The default way to handle this is to just await all initializables.
			//Preferably you'd want this to always run on the main thread, or continue to the main thread
			//but called code could avoid caputring the sync context, so it's out of our control
			foreach(var init in Initializables)
				await init.OnGameInitialized()
					.ConfigureAwait(true);

			isInitializationFinished = true;
		}

		private void Update()
		{
			//The reason we don't update until initialization is finished
			//is because we CAN'T let potential tickables that may depend on
			//initializablizes having run, so to avoid this issue we don't run them until they are init
			if(!isInitializationFinished)
				return;

			foreach(IGameTickable tickable in Tickables)
				tickable.Tick();
		}
	}
}
