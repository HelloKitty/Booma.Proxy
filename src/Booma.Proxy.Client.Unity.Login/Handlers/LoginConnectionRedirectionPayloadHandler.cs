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
		/// <inheritdoc />
		public override async Task HandleMessage(IClientMessageContext<PSOBBLoginPacketPayloadClient> context, LoginConnectionRedirectPayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Redirecting Login to {BuildLoginDebugString(payload)}");

			bool result = await context.ConnectionService.ConnectAsync(payload.EndpointAddress, payload.EndpointerPort);

			if(!result)
				throw new InvalidOperationException($"Failed to connect to redirected endpoint. {BuildLoginDebugString(payload)}");
		}

		private string BuildLoginDebugString(LoginConnectionRedirectPayload payload)
		{
			return $"Ip: {payload.EndpointAddress.ToString()} Port: {payload.EndpointerPort}";
		}
	}
}
