using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public class InvalidPlayerCommandMessageContext : INetworkPlayerFullCommandMessageContext
	{
		public static InvalidPlayerCommandMessageContext Instance { get; } = new InvalidPlayerCommandMessageContext();

		/// <inheritdoc />
		public bool isValid { get; } = false;

		/// <inheritdoc />
		public INetworkPlayer RemotePlayer { get; }

		/// <inheritdoc />
		public INetworkPlayer LocalPlayer { get; }
	}
}
