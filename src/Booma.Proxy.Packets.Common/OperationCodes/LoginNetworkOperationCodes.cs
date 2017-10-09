using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Enumeration of all the login server
	/// operation codes.
	/// </summary>
	public enum LoginNetworkOperationCodes : short
	{
		//TODO: Format
		BB_SECURITY_TYPE = 0x00E6,
		BB_WELCOME_TYPE = 0x0003,
		REDIRECT_TYPE = 0x0019,
		LOGIN_93_TYPE = 0x0093,
		MESSAGE_BOX_TYPE = 0x001A,
		BB_OPTION_REQUEST_TYPE = 0x00E0,
		BB_OPTION_CONFIG_TYPE = 0x00E2
	}
}
