using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	public sealed class DefaultClientPayloadHandler : IPeerPayloadSpecificMessageHandler<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer, IProxiedMessageContext<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>
	{
		private ILog Logger { get; }

		/// <inheritdoc />
		public DefaultClientPayloadHandler(ILog logger)
		{
			Logger = logger;
		}

		/// <inheritdoc />
		public async Task HandleMessage(IProxiedMessageContext<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient> context, PSOBBGamePacketPayloadClient payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Recieved unhandled client payload Name: {payload.GetType().Name} with OpCode: {(GameNetworkOperationCode)payload.OperationCode} - {payload.OperationCode:X}");

			if(payload is IUnknownPayloadType u)
			{
				Logger.Info($"{u.UnknownBytes.Aggregate("", (s, b) => $"{s} 0x{b:X}")}");
			}

			await context.ProxyConnection.SendMessage(payload)
				.ConfigureAwait(false);
		}
	}
}
