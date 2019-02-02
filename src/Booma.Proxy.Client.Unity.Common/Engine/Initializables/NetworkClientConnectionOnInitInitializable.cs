using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.PreShipSelectionScene)]
	[SceneTypeCreate(GameSceneType.CharacterSelectionScreen)] //probably more than just the character screen.
	public sealed class NetworkClientConnectionOnInitInitializable : IGameInitializable
	{
		private IConnectable Connectable { get; }

		private IGameConnectionEndpointDetails ConnectionDetails { get; }

		private ILog Logger { get; }

		/// <inheritdoc />
		public NetworkClientConnectionOnInitInitializable([NotNull] IConnectable connectable, [NotNull] IGameConnectionEndpointDetails connectionDetails, [NotNull] ILog logger)
		{
			Connectable = connectable ?? throw new ArgumentNullException(nameof(connectable));
			ConnectionDetails = connectionDetails ?? throw new ArgumentNullException(nameof(connectionDetails));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Connectiong to: {ConnectionDetails.IpAddress}:{ConnectionDetails.Port}");

			//This initializable actually just
			//connects a IConnectable with the provided game details.
			return Connectable.ConnectAsync(ConnectionDetails.IpAddress, ConnectionDetails.Port);
		}
	}
}
