using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladMMO;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Handler for the <see cref="SharedWelcomePayload"/>.
	/// </summary>
	[PSOBBHandler]
	public class InteropWelcomePayloadHandler : BasePSOBBIncomingInteropPayloadHandler<SharedWelcomePayload>
	{
		/// <summary>
		/// Crypto initialization service that can be init from the welcome message.
		/// </summary>
		private IFullCryptoInitializationService<byte[]> CryptoInitializer { get; }

		public InteropWelcomePayloadHandler(ILog logger,
			[NotNull] IFullCryptoInitializationService<byte[]> cryptoInitializer)
			: base(logger)
		{
			CryptoInitializer = cryptoInitializer ?? throw new ArgumentNullException(nameof(cryptoInitializer));
		}

		/// <inheritdoc />
		public override async Task HandleMessage(InteropPSOBBPeerMessageContext context, SharedWelcomePayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info("Initializing crypto.");

			//the crypto needs to init after the welcome message
			CryptoInitializer.EncryptionInitializable.Initialize(payload.ClientVector);
			CryptoInitializer.DecryptionInitializable.Initialize(payload.ServerVector);

			//After the welcome message is recieved we need to send the login.
			await context.PayloadSendService.SendMessage(BuildLoginPacket());
		}

		protected virtual PSOBBGamePacketPayloadClient BuildLoginPacket()
		{
			return new SharedLoginRequest93Payload(0x41, 0, 0, "admin2", "test", new ClientVerificationData(0x41, new byte[40]), SharedLoginRequest93Payload.ServerType.Ship);
		}
	}
}
