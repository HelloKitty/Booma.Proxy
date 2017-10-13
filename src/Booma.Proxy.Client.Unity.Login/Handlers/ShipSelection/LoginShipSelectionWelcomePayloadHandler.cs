using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class LoginShipSelectionWelcomePayloadHandler : LoginWelcomePayloadHandler
	{
		//This is to keep track of which service we're connected to
		public int welcomeCount = 0;

		/// <inheritdoc />
		protected override PSOBBLoginPacketPayloadClient BuildLoginPacket()
		{
			//If we've done this already then we're talking to the block now
			LoginLoginRequest93Payload.ServerType serverType = welcomeCount == 0 ? LoginLoginRequest93Payload.ServerType.Login : LoginLoginRequest93Payload.ServerType.Ship;

			var payload = new LoginLoginRequest93Payload(0x41, SessionDetails.SessionId, SessionDetails.GuildCardNumber, LoginDetails.Username, LoginDetails.Password, new ClientVerificationData(0x41, SessionDetails.SessionVerificationData), serverType);

			welcomeCount++;
			return payload;
		}
	}
}
