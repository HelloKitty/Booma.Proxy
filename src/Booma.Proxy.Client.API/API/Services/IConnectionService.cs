using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Unified contract for <see cref="IConnectable"/> and
	/// <see cref="IDisconnectable"/> making it easier to consume.
	/// </summary>
	public interface IConnectionService : IDisconnectable, IConnectable
	{
		/// <summary>
		/// Indictates if a connection is established.
		/// </summary>
		bool isConnected { get; }
	}
}
