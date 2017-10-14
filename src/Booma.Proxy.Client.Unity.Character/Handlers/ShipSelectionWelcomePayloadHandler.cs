using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class ShipSelectionWelcomePayloadHandler : SharedWelcomePayloadHandler
	{
		//This is to keep track of which service we're connected to
		[SerializeField]
		public SharedLoginRequest93Payload.ServerType ServerType = SharedLoginRequest93Payload.ServerType.Login;

		/// <inheritdoc />
		protected override PSOBBGamePacketPayloadClient BuildLoginPacket()
		{
			var payload = new SharedLoginRequest93Payload(0x41, SessionDetails.SessionId, SessionDetails.GuildCardNumber, LoginDetails.Username, LoginDetails.Password, new ClientVerificationData(0x41, SessionDetails.SessionVerificationData), ServerType);

			ServerType++;
			return payload;
		}
	}
}
