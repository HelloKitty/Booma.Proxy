using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface ILoginResponseEventSubscribable
	{
		event EventHandler OnLoginSuccess;

		event EventHandler OnLoginFailure;
	}
}
