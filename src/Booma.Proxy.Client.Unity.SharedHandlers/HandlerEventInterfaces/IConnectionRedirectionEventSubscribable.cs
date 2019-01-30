using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for Types that expose a subscribe-able event
	/// for connection redirections.
	/// </summary>
	public interface IConnectionRedirectionEventSubscribable
	{
		event EventHandler OnConnectionRedirection;
	}
}
