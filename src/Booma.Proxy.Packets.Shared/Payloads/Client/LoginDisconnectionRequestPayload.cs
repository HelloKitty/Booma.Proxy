using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// A request to disconnect from the server.
	/// </summary>
	[WireDataContract]
	[LoginClientPacketPayload(LoginNetworkOperationCodes.TYPE_05)]
	public sealed class LoginDisconnectionRequestPayload : PSOBBLoginPacketPayloadClient
	{
		//Empty command payload. Doesn't send any data.


		public LoginDisconnectionRequestPayload()
		{
			
		}
	}
}
