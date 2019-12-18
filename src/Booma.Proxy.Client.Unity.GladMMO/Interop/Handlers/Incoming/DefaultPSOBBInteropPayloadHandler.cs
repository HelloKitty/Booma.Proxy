using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladMMO;
using GladNet;

namespace Booma.Proxy
{
	public sealed class DefaultPSOBBInteropPayloadHandler : IPeerPayloadSpecificMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>
	{
		private ILog Logger { get; }

		public DefaultPSOBBInteropPayloadHandler([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, PSOBBGamePacketPayloadServer payload)
		{
			if(Logger.IsWarnEnabled)
				Logger.Warn($"PSOBB Unhandled Packet: {payload.OperationCode}:{payload.GetType().Name}");
		}
	}
}
