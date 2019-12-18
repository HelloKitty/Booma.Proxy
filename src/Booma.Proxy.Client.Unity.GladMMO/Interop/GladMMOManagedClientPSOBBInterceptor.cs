using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using GladMMO;
using GladNet;
using Nito.AsyncEx;

namespace Booma.Proxy
{
	public sealed class GladMMOManagedClientPSOBBInterceptor : IManagedNetworkClient<GameClientPacketPayload, GameServerPacketPayload>, IPeerRequestSendService<PSOBBGamePacketPayloadClient>
	{
		private ILog Logger { get; }

		private IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> PSOBBNetworkClient { get; }

		private MessageHandlerService<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, InteropPSOBBPeerMessageContext> PSOBBIncomingMessageHandlers { get; }

		private MessageHandlerService<GameClientPacketPayload, PSOBBGamePacketPayloadClient> GladMMOOutgoingMessageHandlers { get; }

		public GladMMOManagedClientPSOBBInterceptor([NotNull] ILog logger, [NotNull] IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> psobbNetworkClient, 
			MessageHandlerService<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, InteropPSOBBPeerMessageContext> psobbIncomingMessageHandlers, 
			MessageHandlerService<GameClientPacketPayload, PSOBBGamePacketPayloadClient> gladMmoOutgoingMessageHandlers)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			PSOBBNetworkClient = psobbNetworkClient ?? throw new ArgumentNullException(nameof(psobbNetworkClient));
			PSOBBIncomingMessageHandlers = psobbIncomingMessageHandlers;
			GladMMOOutgoingMessageHandlers = gladMmoOutgoingMessageHandlers;
		}

		async Task<SendResult> IPeerPayloadSendService<GameClientPacketPayload>.SendMessage<TPayloadType>(TPayloadType payload, DeliveryMethod method = DeliveryMethod.ReliableOrdered)
		{
			await GladMMOOutgoingMessageHandlers.TryHandleMessage(new DefaultPeerMessageContext<PSOBBGamePacketPayloadClient>(PSOBBNetworkClient, PSOBBNetworkClient, this), new NetworkIncomingMessage<GameClientPacketPayload>(new HeaderlessPacketHeader(1), payload))
				.ConfigureAwait(false);

			return SendResult.Sent;
		}

		//TODO: Implement SendImmediately mechanics.
		async Task<SendResult> IPeerPayloadSendService<GameClientPacketPayload>.SendMessageImmediately<TPayloadType>(TPayloadType payload, DeliveryMethod method = DeliveryMethod.ReliableOrdered)
		{
			await GladMMOOutgoingMessageHandlers.TryHandleMessage(new DefaultPeerMessageContext<PSOBBGamePacketPayloadClient>(PSOBBNetworkClient, PSOBBNetworkClient, this), new NetworkIncomingMessage<GameClientPacketPayload>(new HeaderlessPacketHeader(1), payload))
				.ConfigureAwait(false);

			return SendResult.Sent;
		}

		public Task DisconnectAsync(int delay)
		{
			Logger.Debug($"GladMMO disconnect request.");
			return PSOBBNetworkClient.DisconnectAsync(delay);
		}

		public Task<bool> ConnectAsync(string ip, int port)
		{
			Logger.Debug($"GladMMO Connect request: {ip}:{port}");

			//TODO: Hack to connect locally.
			return PSOBBNetworkClient.ConnectAsync("192.168.0.12", 5003);
		}

		//It's important for handling restarting that
		//we indicate accurate connection state
		//when mocking.
		public bool isConnected => PSOBBNetworkClient.isConnected;

		public async Task<NetworkIncomingMessage<GameServerPacketPayload>> ReadMessageAsync(CancellationToken token)
		{
			while (isConnected && !token.IsCancellationRequested)
			{
				NetworkIncomingMessage<PSOBBGamePacketPayloadServer> message = await PSOBBNetworkClient.ReadMessageAsync(token)
					.ConfigureAwait(false);

				await PSOBBIncomingMessageHandlers.TryHandleMessage(new InteropPSOBBPeerMessageContext(PSOBBNetworkClient, PSOBBNetworkClient, this, this), message)
					.ConfigureAwait(false);
			}

			//We don't 1:1 transform packets, that would be too difficult
			//so the above handler API will
			return null;
		}

		public async Task<TResponseType> InterceptPayload<TResponseType>(CancellationToken cancellationToken)
		{
			throw new NotImplementedException("TODO: Implement this stupid stuff.");
		}

		public async Task WriteAsync(byte[] bytes, int offset, int count)
		{
			throw new NotImplementedException("TODO: Implement this stupid stuff.");
		}

		public async Task<TResponseType> SendRequestAsync<TResponseType>(PSOBBGamePacketPayloadClient request, DeliveryMethod method = DeliveryMethod.ReliableOrdered, CancellationToken cancellationToken = new CancellationToken())
		{
			throw new NotImplementedException($"Async message sending is not implemented.");
		}
	}
}
