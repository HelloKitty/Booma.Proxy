using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;

namespace Booma.Proxy
{
	/// <summary>
	/// Handler for the <see cref="LoginWelcomePayload"/>.
	/// </summary>
	[Injectee]
	public sealed class LoginWelcomePayloadHandler : LoginMessageHandler<LoginWelcomePayload>
	{
		/// <summary>
		/// Crypto initialization service that can be init from the welcome message.
		/// </summary>
		[Inject]
		private IFullCryptoInitializationService<byte[]> CryptoInitializer { get; }

		/// <inheritdoc />
		public override async Task HandleMessage(IClientMessageContext<PSOBBLoginPacketPayloadClient> context, LoginWelcomePayload payload)
		{
			//the crypto needs to init after the welcome message
			CryptoInitializer.EncryptionInitializable.Initialize(payload.ClientVector);
			CryptoInitializer.DecryptionInitializable.Initialize(payload.ServerVector);

			//TODO: Abstract the username/pass and version string stuff out
			//After the welcome message is recieved we need to send the login.
			await context.PayloadSendService.SendMessage(new LoginLoginRequest93Payload(0x41, "glader", "playpso69", ClientVerificationData.FromVersionString("Destiny v0.6")));
		}
	}
}
