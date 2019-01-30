using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	[NetworkMessageHandler(GameSceneType.TitleScreen)]
	public sealed class TitleConnectionRedirectionPayloadHandler : SharedConnectionRedirectionPayloadHandler
	{
		private PlayableDirector Director { get; }

		/// <inheritdoc />
		public TitleConnectionRedirectionPayloadHandler(IGameConnectionEndpointDetails connectionEndpoint, IFullCryptoInitializationService<byte[]> cryptoInitializer, ILog logger, [NotNull] PlayableDirector director) 
			: base(connectionEndpoint, cryptoInitializer, logger)
		{
			Director = director ?? throw new ArgumentNullException(nameof(director));
		}

		//Based on old titlescreen version of: https://imgur.com/rAUbC0S
		/// <inheritdoc />
		protected override async Task OnConnectionRedirected()
		{
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
