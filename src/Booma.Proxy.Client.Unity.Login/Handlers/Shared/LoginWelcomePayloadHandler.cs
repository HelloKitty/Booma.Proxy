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
	public class LoginWelcomePayloadHandler : LoginMessageHandler<LoginWelcomePayload>
	{
		/// <summary>
		/// Crypto initialization service that can be init from the welcome message.
		/// </summary>
		[Inject]
		private IFullCryptoInitializationService<byte[]> CryptoInitializer { get; }
		
		/// <summary>
		/// The login details model.
		/// </summary>
		[Inject]
		protected ILoginDetailsModel LoginDetails { get; }

		/// <summary>
		/// The session details model.
		/// </summary>
		[Inject]
		protected ILoginSessionDetails SessionDetails { get; }

		/// <inheritdoc />
		public override async Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, LoginWelcomePayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info("Initializing crypto.");

			//the crypto needs to init after the welcome message
			CryptoInitializer.EncryptionInitializable.Initialize(payload.ClientVector);
			CryptoInitializer.DecryptionInitializable.Initialize(payload.ServerVector);

			//After the welcome message is recieved we need to send the login.
			await context.PayloadSendService.SendMessage(BuildLoginPacket());
		}

		/// <summary>
		/// Builds the login packet.
		/// Overridable by child handlers to allow for mutating the default login packet.
		/// </summary>
		/// <returns></returns>
		protected virtual PSOBBGamePacketPayloadClient BuildLoginPacket()
		{
			return new LoginLoginRequest93Payload(0x41, SessionDetails.SessionId, LoginDetails.Username, LoginDetails.Password, new ClientVerificationData(0x41, SessionDetails.SessionVerificationData));
		}
	}
}
