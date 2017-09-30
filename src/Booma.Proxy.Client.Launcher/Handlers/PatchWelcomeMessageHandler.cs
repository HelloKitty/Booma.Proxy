using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	public sealed class PatchWelcomeMessageHandler : IClientPayloadSpecificMessageHandler<PatchingWelcomePayload, PSOBBPatchPacketPayloadClient>
	{
		/// <summary>
		/// The initialization container that contains the crypto initializers
		/// Theses need to be set before being able to understand future packets.
		/// </summary>
		public IFullCryptoInitializationService<uint> Initializers { get; }

		public PatchWelcomeMessageHandler([NotNull] IFullCryptoInitializationService<uint> initializers)
		{
			if(initializers == null) throw new ArgumentNullException(nameof(initializers));

			Initializers = initializers;
		}

		public async Task HandleMessage(IClientMessageContext<PSOBBPatchPacketPayloadClient> context, PatchingWelcomePayload payload)
		{
			//We need to init the crypto before we can even send the following payload
			Initializers.DecryptionInitializable.Initialize(payload.ServerVector);
			Initializers.EncryptionInitializable.Initialize(payload.ClientVector);

			//Sends an ack to let the patch server know we recieved the welcome and that we've established encryption.
			await context.PayloadSendService.SendMessage(new PatchingWelcomeAckPayload());
		}
	}
}
