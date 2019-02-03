using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class DefaultConnectionRedirector : IConnectionRedirector
	{
		private IGameObjectComponentAttachmentFactory ComponentAttachmentFactory { get; }

		/// <summary>
		/// The connection/reconnection/redirection service.
		/// </summary>
		private IConnectionService ConnectionService { get; }

		private IGameConnectionEndpointDetails ConnectionDetails { get; }

		/// <inheritdoc />
		public DefaultConnectionRedirector([NotNull] IGameObjectComponentAttachmentFactory componentAttachmentFactory, [NotNull] IConnectionService connectionService, [NotNull] IGameConnectionEndpointDetails connectionDetails)
		{
			ComponentAttachmentFactory = componentAttachmentFactory ?? throw new ArgumentNullException(nameof(componentAttachmentFactory));
			ConnectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
			ConnectionDetails = connectionDetails ?? throw new ArgumentNullException(nameof(connectionDetails));
		}

		/// <inheritdoc />
		public async Task RedirectAsync()
		{
			ConnectionService.Disconnect();

			await Task.Delay(1000);

			GameNetworkClient client = ComponentAttachmentFactory.AddTo<GameNetworkClient>(new GameObject("Runtime Redirected NetworkClient"));

			//It's important we connect THROUGH this client, not the connection service so that
			//it starts the network thread
			await client.ConnectAsync(ConnectionDetails.IpAddress, ConnectionDetails.Port);
		}
	}
}
