using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface ILoginResponseEventSubscribable
	{
		//TODO: Provide actual args/data for reasons and such.
		event EventHandler<LoginResultEventArgs> OnLoginProcessResult;
	}

	public class LoginResultEventArgs
	{
		/// <summary>
		/// The result of authentication/login.
		/// </summary>
		public AuthenticationResponseCode AuthenticationResult { get; }

		public bool isSuccessful => AuthenticationResult == AuthenticationResponseCode.LOGIN_93BB_OK;

		/// <inheritdoc />
		public LoginResultEventArgs(AuthenticationResponseCode authenticationResult)
		{
			if(!Enum.IsDefined(typeof(AuthenticationResponseCode), authenticationResult)) throw new InvalidEnumArgumentException(nameof(authenticationResult), (int)authenticationResult, typeof(AuthenticationResponseCode));
			
			AuthenticationResult = authenticationResult;
		}
	}
}
