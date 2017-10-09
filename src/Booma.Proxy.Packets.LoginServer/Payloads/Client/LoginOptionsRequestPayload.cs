using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/// <summary>
	/// Empty payload the client sends to get character specific options data
	/// that are saved on the server.
	/// </summary>
	[WireDataContract]
	[LoginClientPacketPayload(LoginNetworkOperationCodes.BB_OPTION_REQUEST_TYPE)]
	public sealed class LoginOptionsRequestPayload : PSOBBLoginPacketPayloadClient
	{
		//Empty, just a payload that alerts the server to the desire of options

		public LoginOptionsRequestPayload()
		{
			
		}
	}
}
