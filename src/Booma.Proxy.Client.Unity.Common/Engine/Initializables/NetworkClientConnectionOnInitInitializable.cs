using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.CharacterSelectionScreen)] //probably more than just the character screen.
	public sealed class NetworkClientConnectionOnInitInitializable : IGameInitializable
	{
		private IConnectable Connectable { get; }

		private IGameConnectionEndpointDetails ConnectionDetails { get; }

		/// <inheritdoc />
		public NetworkClientConnectionOnInitInitializable([NotNull] IConnectable connectable, [NotNull] IGameConnectionEndpointDetails connectionDetails)
		{
			Connectable = connectable ?? throw new ArgumentNullException(nameof(connectable));
			ConnectionDetails = connectionDetails ?? throw new ArgumentNullException(nameof(connectionDetails));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			//This initializable actually just
			//connects a IConnectable with the provided game details.
			return Connectable.ConnectAsync(ConnectionDetails.IpAddress, ConnectionDetails.Port);
		}
	}
}
