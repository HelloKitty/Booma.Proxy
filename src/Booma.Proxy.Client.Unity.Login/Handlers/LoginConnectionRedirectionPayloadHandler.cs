using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;

namespace Booma.Proxy.Handlers
{
	[Injectee]
	public sealed class LoginConnectionRedirectionPayloadHandler : LoginMessageHandler<LoginConnectionRedirectPayload>
	{
		[Inject]
		private IFullCryptoInitializationService<byte[]> CryptoInitializer { get; }

		/// <inheritdoc />
		public override async Task HandleMessage(IClientMessageContext<PSOBBLoginPacketPayloadClient> context, LoginConnectionRedirectPayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Redirecting Login to {BuildLoginDebugString(payload)}");

			//Have to clear crypto since we're connecting to a new endpoint
			CryptoInitializer.DecryptionInitializable.Uninitialize();
			CryptoInitializer.EncryptionInitializable.Uninitialize();

			bool result = await context.ConnectionService.ConnectAsync(payload.EndpointAddress, payload.EndpointerPort);

			if(!result)
				throw new InvalidOperationException($"Failed to connect to redirected endpoint. {BuildLoginDebugString(payload)}");
		}

		private string BuildLoginDebugString(LoginConnectionRedirectPayload payload)
		{
			return $"Ip: {payload.EndpointAddress} Port: {payload.EndpointerPort}";
		}
	}
}
