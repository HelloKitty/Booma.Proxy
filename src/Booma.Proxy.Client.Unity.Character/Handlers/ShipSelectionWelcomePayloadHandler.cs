using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class ShipSelectionWelcomePayloadHandler : SharedWelcomePayloadHandler
	{
		/// <inheritdoc />
		public ShipSelectionWelcomePayloadHandler(ILog logger) 
			: base(logger)
		{

		}

		/// <inheritdoc />
		protected override PSOBBGamePacketPayloadClient BuildLoginPacket()
		{
			var payload = new SharedLoginRequest93Payload(0x41, SessionDetails.SessionId, SessionDetails.GuildCardNumber, LoginDetails.Username, LoginDetails.Password, new ClientVerificationData(0x41, SessionDetails.SessionVerificationData), AuthType);

			AuthType++;
			return payload;
		}
	}
}
