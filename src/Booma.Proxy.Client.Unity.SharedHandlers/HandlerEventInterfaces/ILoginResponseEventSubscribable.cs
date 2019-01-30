using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface ILoginResponseEventSubscribable
	{
		//TODO: Provide actual args/data for reasons and such.
		event EventHandler OnLoginSuccess;

		event EventHandler OnLoginFailure;
	}
}
