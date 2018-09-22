using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	public sealed class SharedWelcomePayloadServerHandler : BaseGameServerPayloadHandler<SharedWelcomePayload, PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadServer>
	{
		/// <inheritdoc />
		public SharedWelcomePayloadServerHandler(ILog logger) 
			: base(logger)
		{
		}

		/// <inheritdoc />
		public override async Task OnHandleMessage(IProxiedMessageContext<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadServer> context, SharedWelcomePayload payload)
		{
			Console.WriteLine($"Recieved Welcome: {payload.CopyrightMessage}");

			await context.ProxyConnection.SendMessage(payload)
				.ConfigureAwait(false);
		}
	}
}
