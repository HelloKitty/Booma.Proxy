using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.ServerSelectionScreen)]
	public sealed class CharacterToShipConnectionRedirectorEventListener : BaseSingleEventListenerInitializable<IConnectionRedirectionEventSubscribable>
	{
		private IConnectionRedirector Reconnector { get; }

		private ILog Logger { get; }

		/// <inheritdoc />
		public CharacterToShipConnectionRedirectorEventListener([NotNull] IConnectionRedirectionEventSubscribable subscriptionService, [NotNull] IConnectionRedirector reconnector, [NotNull] ILog logger) : base(subscriptionService)
		{
			Reconnector = reconnector ?? throw new ArgumentNullException(nameof(reconnector));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		protected override async void OnEventFired(object source, EventArgs args)
		{
			//We just wanna redirect, that's all.
			await Reconnector.RedirectAsync();

			//The way Character to Ship redirection works
			//is that it shouldn't redirect again on next redirect
			//we'll still be in the same scene
			//but we need to NOT handle this again, ship to block handler will
			//exist
			Unsubscribe();
		}
	}
}
