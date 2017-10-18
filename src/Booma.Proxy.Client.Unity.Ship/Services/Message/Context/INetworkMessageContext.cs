using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Base contract for network message context objects.
	/// </summary>
	public interface INetworkMessageContext
	{
		/// <summary>
		/// Indicates if the model/context is in a valid state.
		/// </summary>
		bool isValid { get; }
	}
}
