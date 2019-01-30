using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.PreBlockBurstingScene)]
	public sealed class PreBlockWelcomePayloadHandler : SharedWelcomePayloadHandler
	{
		/// <inheritdoc />
		public PreBlockWelcomePayloadHandler(IFullCryptoInitializationService<byte[]> cryptoInitializer, IAuthenticationDetailsModel loginDetails, IClientSessionDetails sessionDetails, ILog logger) 
			: base(cryptoInitializer, loginDetails, sessionDetails, logger)
		{
			this.AuthType = SharedLoginRequest93Payload.ServerType.Ship;
		}
	}
}
