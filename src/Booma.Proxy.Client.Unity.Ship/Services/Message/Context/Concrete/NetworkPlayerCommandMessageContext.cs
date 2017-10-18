using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class NetworkPlayerCommandMessageContext : INetworkPlayerFullNetworkMessageContext
	{
		/// <inheritdoc />
		public bool isValid { get; } = true;

		/// <inheritdoc />
		public INetworkPlayer RemotePlayer { get; }

		/// <inheritdoc />
		public INetworkPlayer LocalPlayer { get; }

		/// <inheritdoc />
		public NetworkPlayerCommandMessageContext(INetworkPlayer remote, INetworkPlayer local)
		{
			if(remote == null) throw new ArgumentNullException(nameof(remote));
			if(local == null) throw new ArgumentNullException(nameof(local));

			RemotePlayer = remote;
			LocalPlayer = local;
		}
	}
}
