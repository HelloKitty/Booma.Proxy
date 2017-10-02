using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	[LoginServerPacketPayload(LoginNetworkOperationCode.MESSAGE_BOX_TYPE)]
	public sealed class LoginCreateMessageBoxEventPayload : PSOBBLoginPacketPayloadServer
	{
		/// <summary>
		/// Message to write to the created message box.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[WireMember(1)]
		public string Message { get; }

		//serializer ctor
		private LoginCreateMessageBoxEventPayload()
		{
			
		}
	}
}
