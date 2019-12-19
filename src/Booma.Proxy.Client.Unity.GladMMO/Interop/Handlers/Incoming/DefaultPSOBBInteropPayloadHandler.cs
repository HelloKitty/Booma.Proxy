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

			if (payload is ISub60CommandContainer sub60)
			{
				Logger.Info($"PSOBB Unhandled Packet: 0x60 {sub60.Command.CommandOperationCode:x} {sub60}");
			}
			else if (payload is ISub62CommandContainer sub62)
			{
				Logger.Info($"PSOBB Unhandled Packet: 0x62 {sub62.Command.CommandOperationCode:x} {sub62}");
			}
			else if (payload is ISub6DCommandContainer sub6D)
			{
				Logger.Info($"PSOBB Unhandled Packet: 0x62 {sub6D.Command.CommandOperationCode:x} {sub6D}");
			}
			else
			{
				if(Logger.IsWarnEnabled)
					if(payload is IUnknownPayloadType unk)
						Logger.Info($"PSOBB Unhandled Packet: 0x{payload.OperationCode:x}:{unk.ToString()}");
					else
						Logger.Info($"PSOBB Unhandled Packet of Type: 0x{payload.OperationCode:x}:{payload.GetType().Name} Info: {payload}");
			}
		}
	}
}
