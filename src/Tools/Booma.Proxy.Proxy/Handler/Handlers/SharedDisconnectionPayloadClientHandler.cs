using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	public sealed class SharedDisconnectionPayloadClientHandler : BaseGameServerPayloadHandler<SharedDisconnectionRequestPayload, PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer>
	{
		private IFullCryptoInitializationService<byte[]> CryptoInitializable { get; }

		/// <inheritdoc />
		public SharedDisconnectionPayloadClientHandler(ILog logger, [NotNull] IFullCryptoInitializationService<byte[]> cryptoInitializable)
			: base(logger)
		{
			CryptoInitializable = cryptoInitializable ?? throw new ArgumentNullException(nameof(cryptoInitializable));
		}

		/// <inheritdoc />
		public override async Task OnHandleMessage(IProxiedMessageContext<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient> context, SharedDisconnectionRequestPayload payload)
		{
			//When we disconnect we must also clear the crypto data because they may reconnect and it's shared in the proxy
			CryptoInitializable.DecryptionInitializable.Uninitialize();
			CryptoInitializable.EncryptionInitializable.Uninitialize();

			//We should forward that we want the server to disconnect us, but we should disconnect the client right away and not wait.
			await context.ProxyConnection.SendMessage(payload)
				.ConfigureAwait(false);

			//Disconnects the client.
			await context.ConnectionService.DisconnectAsync(0)
				.ConfigureAwait(false);
		}
	}
}
