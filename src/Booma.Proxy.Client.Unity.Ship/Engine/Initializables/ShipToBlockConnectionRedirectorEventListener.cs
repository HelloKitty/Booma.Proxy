using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	//The way this works is there are multiple of this listener in the scene.
	[SceneTypeCreate(GameSceneType.ServerSelectionScreen)]
	public sealed class ShipToBlockConnectionRedirectorEventListener : BaseSingleEventListenerInitializable<IConnectionRedirectionEventSubscribable>
	{
		private ILog Logger { get; }

		private bool shouldIgnoreRedirection = true;

		/// <inheritdoc />
		public ShipToBlockConnectionRedirectorEventListener([NotNull] IConnectionRedirectionEventSubscribable subscriptionService, ILog logger) : base(subscriptionService)
		{
			Logger = logger;
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, EventArgs args)
		{
			//We ignore the first one in the server selection
			//scene because the first redirect is a
			//redirection from Character to Ship
			//and the scene doesn't change so we're still in it.
			if(shouldIgnoreRedirection)
			{
				shouldIgnoreRedirection = false;
				return;
			}

			//TODO: Don't just load a scene here like this
			//We're suppose to load scene 4 per the old design
			//rigged up in the editor.
			SceneManager.LoadSceneAsync(4).allowSceneActivation = true;


			//We don't need to, but might as well.
			Unsubscribe();
		}
	}
}
