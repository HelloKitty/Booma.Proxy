using System;
using System.Collections.Generic;
using System.Text;
using GladMMO;
using GladNet;

namespace Booma.Proxy
{
	public sealed class InteropPSOBBPeerMessageContext : IPeerMessageContext<PSOBBGamePacketPayloadClient>
	{
		public IConnectionService ConnectionService { get; }

		public IPeerPayloadSendService<PSOBBGamePacketPayloadClient> PayloadSendService { get; }

		public IPeerRequestSendService<PSOBBGamePacketPayloadClient> RequestSendService { get; }

		public IPeerPayloadSendService<GameServerPacketPayload> GladMMOClientPayloadReceiver { get; }

		public InteropPSOBBPeerMessageContext([NotNull] IConnectionService connectionService, [NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> payloadSendService, [NotNull] IPeerRequestSendService<PSOBBGamePacketPayloadClient> requestSendService, [NotNull] IPeerPayloadSendService<GameServerPacketPayload> gladMmoClientPayloadReceiver)
		{
			ConnectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
			PayloadSendService = payloadSendService ?? throw new ArgumentNullException(nameof(payloadSendService));
			RequestSendService = requestSendService ?? throw new ArgumentNullException(nameof(requestSendService));
			GladMMOClientPayloadReceiver = gladMmoClientPayloadReceiver ?? throw new ArgumentNullException(nameof(gladMmoClientPayloadReceiver));
		}
	}
}
