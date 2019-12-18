using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladMMO;
using GladNet;

namespace Booma.Proxy
{
	public sealed class DefaultPSOBBInteropPayloadHandler : IPeerPayloadSpecificMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, InteropPSOBBPeerMessageContext>
	{
		private ILog Logger { get; }

		public DefaultPSOBBInteropPayloadHandler([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task HandleMessage(InteropPSOBBPeerMessageContext context, PSOBBGamePacketPayloadServer payload)
		{
			if(Logger.IsWarnEnabled)
				Logger.Warn($"PSOBB Unhandled Packet: 0x{payload.OperationCode:x}:{payload.GetType().Name}");
		}
	}
}
