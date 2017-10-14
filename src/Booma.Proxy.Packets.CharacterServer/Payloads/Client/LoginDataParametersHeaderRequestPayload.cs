using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload sent to request the data parameters.
	/// Such as: PMT, battle parameters, etc
	/// </summary>
	[WireDataContract]
	[LoginClientPacketPayload(LoginNetworkOperationCodes.BB_PARAM_HEADER_REQ_TYPE)]
	public sealed class LoginDataParametersHeaderRequestPayload : PSOBBLoginPacketPayloadClient
	{
		//Just a command payload. Nothing to implement

		public LoginDataParametersHeaderRequestPayload()
		{
			
		}
	}
}
