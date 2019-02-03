using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace Booma.Proxy
{
	//TODO: This is sooo a hack but I don't have the patience to deal with this right now.
	public sealed class GlobalConnectionService : IConnectionService
	{
		/// <inheritdoc />
		public Task DisconnectAsync(int delay)
		{
			return GameNetworkClient.CurrentConnectionService.DisconnectAsync(delay);
		}

		/// <inheritdoc />
		public Task<bool> ConnectAsync(string ip, int port)
		{
			return GameNetworkClient.CurrentConnectionService.ConnectAsync(ip, port);
		}

		/// <inheritdoc />
		public bool isConnected => GameNetworkClient.CurrentConnectionService.isConnected;
	}
}
