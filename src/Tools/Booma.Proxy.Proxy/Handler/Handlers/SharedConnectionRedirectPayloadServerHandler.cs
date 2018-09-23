using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	public sealed class SharedConnectionRedirectPayloadServerHandler : BaseGameServerPayloadHandler<SharedConnectionRedirectPayload, PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>
	{
		private IFullCryptoInitializationService<byte[]> CryptoInitializable { get; } 

		/// <inheritdoc />
		public SharedConnectionRedirectPayloadServerHandler(ILog logger, [NotNull] IFullCryptoInitializationService<byte[]> cryptoInitializable) 
			: base(logger)
		{
			CryptoInitializable = cryptoInitializable ?? throw new ArgumentNullException(nameof(cryptoInitializable));
		}

		/// <inheritdoc />
		public override async Task OnHandleMessage(IProxiedMessageContext<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> context, SharedConnectionRedirectPayload payload)
		{
			//5001 is the port used when we are running the character server and login server starting from 5000
			//so if we get a redirect to 5001 we should wire it to 12001 instead
			switch(payload.EndpointerPort)
			{
				case 5001:
					payload = new SharedConnectionRedirectPayload(payload.EndpointAddress, 12001);
					break;
				case 5002:
					payload = new SharedConnectionRedirectPayload(payload.EndpointAddress, 5278);
					break;
				case 5003:
					payload = new SharedConnectionRedirectPayload(payload.EndpointAddress, 5279);
					break;
			}

			//TODO: Handle port mapping
			//Just forward the redirect, also disconnect the client after uninitializing the crypto
			await context.ProxyConnection.SendMessageImmediately(payload)
				.ConfigureAwait(false);

			CryptoInitializable.EncryptionInitializable.Uninitialize();
			CryptoInitializable.DecryptionInitializable.Uninitialize();

			await context.ProxyConnection.DisconnectAsync(1)
				.ConfigureAwait(false);
		}
	}
}
