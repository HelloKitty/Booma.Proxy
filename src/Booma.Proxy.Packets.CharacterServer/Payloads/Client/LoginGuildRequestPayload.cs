using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//TODO: What is this packet for/about?
	[WireDataContract]
	[LoginClientPacketPayload(LoginNetworkOperationCodes.BB_GUILD_REQUEST_TYPE)]
	public class LoginGuildRequestPayload : PSOBBLoginPacketPayloadClient
	{
		//Empty payload that requests guild stuff? Idk

		public LoginGuildRequestPayload()
		{
			
		}
	}
}
