using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.PreBlockBurstingScene)]
	[SceneTypeCreate(GameSceneType.ServerSelectionScreen)]
	[SceneTypeCreate(GameSceneType.PreShipSelectionScene)]
	[SceneTypeCreate(GameSceneType.CharacterSelectionScreen)] //probably more than just the character screen.
	public sealed class NetworkClientConnectionOnInitInitializable : IGameInitializable
	{
		private IConnectionService ConnectionService { get; }

		private IGameConnectionEndpointDetails ConnectionDetails { get; }

		private ILog Logger { get; }

		/// <inheritdoc />
		public NetworkClientConnectionOnInitInitializable([NotNull] IConnectionService connectionService, [NotNull] IGameConnectionEndpointDetails connectionDetails, [NotNull] ILog logger)
		{
			ConnectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
			ConnectionDetails = connectionDetails ?? throw new ArgumentNullException(nameof(connectionDetails));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Connectiong to: {ConnectionDetails.IpAddress}:{ConnectionDetails.Port}");

			//This initializable actually just
			//connects a IConnectionService with the provided game details.
			return ConnectionService.ConnectAsync(ConnectionDetails.IpAddress, ConnectionDetails.Port);
		}
	}
}
