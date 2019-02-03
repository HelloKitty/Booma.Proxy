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
		private IGameConnectionEndpointDetails ConnectionDetails { get; }

		private IConnectable Connectable { get; }

		private ILog Logger { get; }

		/// <inheritdoc />
		public CharacterToShipConnectionRedirectorEventListener(IConnectionRedirectionEventSubscribable subscriptionService, [NotNull] IGameConnectionEndpointDetails connectionDetails, [NotNull] IConnectable connectable, [NotNull] ILog logger) 
			: base(subscriptionService)
		{
			ConnectionDetails = connectionDetails ?? throw new ArgumentNullException(nameof(connectionDetails));
			Connectable = connectable ?? throw new ArgumentNullException(nameof(connectable));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		protected override async void OnEventFired(object source, EventArgs args)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Redirecting Connection: {ConnectionDetails.IpAddress}:{ConnectionDetails.Port}");

			await Connectable.ConnectAsync(ConnectionDetails.IpAddress, ConnectionDetails.Port);

			//The way Character to Ship redirection works
			//is that it shouldn't redirect again on next redirect
			//we'll still be in the same scene
			//but we need to NOT handle this again, ship to block handler will
			//exist
			Unsubscribe();
		}
	}
}
