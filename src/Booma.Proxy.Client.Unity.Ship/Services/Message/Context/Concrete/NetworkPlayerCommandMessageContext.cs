using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class NetworkPlayerCommandMessageContext : INetworkPlayerFullCommandMessageContext
	{
		/// <inheritdoc />
		public bool isValid { get; } = true;

		/// <inheritdoc />
		public INetworkPlayer Remote { get; }

		/// <inheritdoc />
		public INetworkPlayer Local { get; }

		/// <inheritdoc />
		public NetworkPlayerCommandMessageContext(INetworkPlayer remote, INetworkPlayer local)
		{
			if(remote == null) throw new ArgumentNullException(nameof(remote));
			if(local == null) throw new ArgumentNullException(nameof(local));

			Remote = remote;
			Local = local;
		}
	}
}
