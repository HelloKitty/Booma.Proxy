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
	public abstract class BaseGladMMOOutgoingInteropPayloadHandler<TSpecificPayloadType> : IPeerMessageHandler<GameClientPacketPayload, PSOBBGamePacketPayloadClient>
		where TSpecificPayloadType : GameClientPacketPayload
	{
		protected ILog Logger { get; }

		/// <inheritdoc />
		protected BaseGladMMOOutgoingInteropPayloadHandler(ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		//TODO: Add exception logging support
		public abstract Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, TSpecificPayloadType payload);

		public bool CanHandle(NetworkIncomingMessage<GameClientPacketPayload> message)
		{
			return message.Payload is TSpecificPayloadType;
		}

		public async Task<bool> TryHandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, NetworkIncomingMessage<GameClientPacketPayload> message)
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
