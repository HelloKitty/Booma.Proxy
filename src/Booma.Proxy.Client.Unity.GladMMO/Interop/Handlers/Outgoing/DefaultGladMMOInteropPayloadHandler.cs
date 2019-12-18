using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladMMO;
using GladNet;

namespace Booma.Proxy
{
	public sealed class DefaultGladMMOInteropPayloadHandler : IPeerPayloadSpecificMessageHandler<GameClientPacketPayload, PSOBBGamePacketPayloadClient>
	{
		private ILog Logger { get; }

		public DefaultGladMMOInteropPayloadHandler([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, GameClientPacketPayload payload)
		{
			if(Logger.IsWarnEnabled)
				Logger.Warn($"GladMMO Unhandled Packet: {payload.GetType().Name}");
		}
	}
}
