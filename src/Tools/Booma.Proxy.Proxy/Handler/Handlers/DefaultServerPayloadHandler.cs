using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	public sealed class DefaultServerPayloadHandler : IPeerPayloadSpecificMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, IProxiedMessageContext<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer>>
	{
		private ILog Logger { get; }

		/// <inheritdoc />
		public DefaultServerPayloadHandler(ILog logger)
		{
			Logger = logger;
		}

		/// <inheritdoc />
		public async Task HandleMessage(IProxiedMessageContext<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> context, PSOBBGamePacketPayloadServer payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Recieved unhandled server payload with OpCode: {(GameNetworkOperationCode)payload.OperationCode} - {payload.OperationCode:X}");

			await context.ProxyConnection.SendMessage(payload)
				.ConfigureAwait(false);
		}
	}
}
