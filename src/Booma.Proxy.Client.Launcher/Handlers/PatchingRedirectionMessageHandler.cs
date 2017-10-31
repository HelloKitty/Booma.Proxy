using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	public sealed class PatchingRedirectionMessageHandler : IPeerPayloadSpecificMessageHandler<PatchingRedirectPayload, PSOBBPatchPacketPayloadClient>
	{
		public IFullCryptoInitializationService<uint> CryptoKeyInitializables { get; }

		/// <inheritdoc />
		public PatchingRedirectionMessageHandler([NotNull] IFullCryptoInitializationService<uint> cryptoKeyInitializables)
		{
			if(cryptoKeyInitializables == null) throw new ArgumentNullException(nameof(cryptoKeyInitializables));

			CryptoKeyInitializables = cryptoKeyInitializables;
		}

		/// <inheritdoc />
		public async Task HandleMessage(IPeerMessageContext<PSOBBPatchPacketPayloadClient> context, PatchingRedirectPayload payload)
		{
			//We must disable the keys since we no longer need it
			//and won't understand the server if it responds and we try to decrypt with
			//the old keys
			CryptoKeyInitializables.DecryptionInitializable.Uninitialize();
			CryptoKeyInitializables.EncryptionInitializable.Uninitialize();

			//This payload indicates we need to connect to another endpoint.
			//In this case it'll be the patch service that provides patching files
			await context.ConnectionService.ConnectAsync(new IPAddress(payload.IPAddress), payload.Port);
		}
	}
}
