using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladMMO;
using GladNet;

namespace Booma.Proxy
{
	/// <summary>
	/// Base handler for all game handlers.
	/// </summary>
	/// <typeparam name="TSpecificPayloadType"></typeparam>
	public abstract class BasePSOBBIncomingInteropPayloadHandler<TSpecificPayloadType> : IPeerMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, InteropPSOBBPeerMessageContext>
		where TSpecificPayloadType : PSOBBGamePacketPayloadServer
	{
		protected ILog Logger { get; }

		/// <inheritdoc />
		protected BasePSOBBIncomingInteropPayloadHandler(ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		//TODO: Add exception logging support
		public abstract Task HandleMessage(InteropPSOBBPeerMessageContext context, TSpecificPayloadType payload);

		public bool CanHandle(NetworkIncomingMessage<PSOBBGamePacketPayloadServer> message)
		{
			return message.Payload is TSpecificPayloadType;
		}

		public async Task<bool> TryHandleMessage(InteropPSOBBPeerMessageContext context, NetworkIncomingMessage<PSOBBGamePacketPayloadServer> message)
		{
			if(!CanHandle(message))
				return false;

			await HandleMessage(context, (TSpecificPayloadType)message.Payload)
				.ConfigureAwait(false);

			return true;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"GameHandler: {GetType().Name} for Payload: {typeof(TSpecificPayloadType).Name}";
		}
	}
}
