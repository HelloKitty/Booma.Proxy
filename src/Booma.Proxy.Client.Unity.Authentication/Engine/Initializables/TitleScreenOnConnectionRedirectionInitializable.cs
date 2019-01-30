using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.TitleScreen)]
	public sealed class TitleScreenOnConnectionRedirectionInitializable : IGameInitializable
	{
		private IConnectionRedirectionEventSubscribable OnConnectionRedirectionSubcriptionService { get; }

		//TODO: Don't expose Unity directors directly.
		private PlayableDirector Director { get; }

		/// <inheritdoc />
		public TitleScreenOnConnectionRedirectionInitializable([NotNull] IConnectionRedirectionEventSubscribable onConnectionRedirectionSubcriptionService, [NotNull] PlayableDirector director)
		{
			OnConnectionRedirectionSubcriptionService = onConnectionRedirectionSubcriptionService ?? throw new ArgumentNullException(nameof(onConnectionRedirectionSubcriptionService));
			Director = director ?? throw new ArgumentNullException(nameof(director));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			OnConnectionRedirectionSubcriptionService.OnConnectionRedirection += OnConnectionDirection;
			return Task.CompletedTask;
		}

		//TODO: Is this afe to do an async eventhandler? It won't capture exceptions and such.
		public async void OnConnectionDirection(object sender, EventArgs eventArgs)
		{
			//Joins this to the main thread first.
			await new UnityYieldAwaitable();

			//For titlescreen, when we're finally redirected to a new connection
			//it's going to be the character connection
			Director.Play();

			//We do a wait so the screen is mostly black before we attempt to async load the scene
			await Task.Delay(2000);

			AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(1);
			loadSceneAsync.allowSceneActivation = false;

			await Task.Delay(2500);

			loadSceneAsync.allowSceneActivation = true;
		}
	}
}
