using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Context for messages that come from other <see cref="INetworkPlayer"/>s.
	/// </summary>
	public interface INetworkPlayerNetworkMessageContext : INetworkMessageContext
	{
		/// <summary>
		/// The <see cref="INetworkPlayer"/> that the message is about
		/// or is the source of.
		/// </summary>
		INetworkPlayer RemotePlayer { get; }
	}
}
