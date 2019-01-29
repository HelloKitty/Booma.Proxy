using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Handler for the <see cref="SharedWelcomePayload"/>.
	/// </summary>
	public class SharedWelcomePayloadHandler : GameMessageHandler<SharedWelcomePayload>
	{
		//TODO: Remove
		private SharedLoginRequest93Payload.ServerType _AuthType;

		/// <summary>
		/// Crypto initialization service that can be init from the welcome message.
		/// </summary>
		private IFullCryptoInitializationService<byte[]> CryptoInitializer { get; }
		
		/// <summary>
		/// The login details model.
		/// </summary>
		protected IAuthenticationDetailsModel LoginDetails { get; }

		/// <summary>
		/// The session details model.
		/// </summary>
		protected IClientSessionDetails SessionDetails { get; }

		//TODO: Fix this
		//[PropertyTooltip("Optional auth type that can be changed. If you're connected to a ship/block you should use the ship auth type.")]
		//[OdinSerialize]
		public SharedLoginRequest93Payload.ServerType AuthType
		{
			get => throw new NotSupportedException("MUST PROPERLY IMPLEMENT NEW CTOR WORKFLOW.");
			protected set => throw new NotSupportedException("MUST PROPERLY IMPLEMENT NEW CTOR WORKFLOW.");
			//first value
		} /*SharedLoginRequest93Payload.ServerType.PreShip*/

		/// <inheritdoc />
		public SharedWelcomePayloadHandler(IFullCryptoInitializationService<byte[]> cryptoInitializer, IAuthenticationDetailsModel loginDetails, IClientSessionDetails sessionDetails, ILog logger) 
			: base(logger)
		{
			CryptoInitializer = cryptoInitializer;
			LoginDetails = loginDetails;
			SessionDetails = sessionDetails;
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, SharedWelcomePayload payload)
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
			return new SharedLoginRequest93Payload(0x41, SessionDetails.SessionId, SessionDetails.GuildCardNumber, LoginDetails.Username, LoginDetails.Password, new ClientVerificationData(0x41, SessionDetails.SessionVerificationData), AuthType);
		}
	}
}
