using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using GladMMO;

namespace Booma.Proxy
{
	//This is a compatibility layer interface that intercepts outgoing GladMMO client packet payloads
	//and translates them into viable PSOBB emulated packets.
	public sealed class GladMMONetworkClientPSOBBPayloadIntercepterSendService : IPeerPayloadSendService<GameClientPacketPayload>
	{
		private ILog Logger { get; }

		public GladMMONetworkClientPSOBBPayloadIntercepterSendService([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		async Task<SendResult> IPeerPayloadSendService<GameClientPacketPayload>.SendMessage<TPayloadType>(TPayloadType payload, DeliveryMethod method)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"GladMMO PSOBB payload intercepted: {payload.GetType().Name}");

			return SendResult.Enqueued;
		}

		async Task<SendResult> IPeerPayloadSendService<GameClientPacketPayload>.SendMessageImmediately<TPayloadType>(TPayloadType payload, DeliveryMethod method)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"GladMMO PSOBB payload intercepted: {payload.GetType().Name}");

			return SendResult.Sent;
		}
	}
}
