using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using UnityEngine;

namespace Booma.Proxy
{
	[NetworkMessageHandler(GameSceneType.ServerSelectionScreen)]
	public sealed class ShipSelectionWelcomePayloadHandler : SharedWelcomePayloadHandler
	{
		public ShipSelectionWelcomePayloadHandler(IFullCryptoInitializationService<byte[]> cryptoInitializer, IAuthenticationDetailsModel loginDetails, IClientSessionDetails sessionDetails, ILog logger) 
			: base(cryptoInitializer, loginDetails, sessionDetails, logger)
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
