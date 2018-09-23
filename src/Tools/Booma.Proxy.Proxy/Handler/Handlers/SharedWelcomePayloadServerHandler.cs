using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	public sealed class SharedWelcomePayloadServerHandler : BaseGameServerPayloadHandler<SharedWelcomePayload, PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>
	{
		private IFullCryptoInitializationService<byte[]> CryptoInitializable { get; } 

		/// <inheritdoc />
		public SharedWelcomePayloadServerHandler(ILog logger, [NotNull] IFullCryptoInitializationService<byte[]> cryptoInitializable) 
			: base(logger)
		{
			CryptoInitializable = cryptoInitializable ?? throw new ArgumentNullException(nameof(cryptoInitializable));
		}

		/// <inheritdoc />
		public override async Task OnHandleMessage(IProxiedMessageContext<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> context, SharedWelcomePayload payload)
		{
			Console.WriteLine($"Recieved Welcome: {payload.CopyrightMessage}");

			//Send first, or else client will not understand because it doesn't have encryption IV yet.
			await context.ProxyConnection.SendMessage(payload)
				.ConfigureAwait(false);

			CryptoInitializable.EncryptionInitializable.Initialize(payload.ClientVector);
			CryptoInitializable.DecryptionInitializable.Initialize(payload.ServerVector);
		}
	}
}
