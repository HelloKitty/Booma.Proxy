using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Proxy
{
	[AdditionalRegisterationAs(typeof(IConnectionRedirectionEventSubscribable))]
	[SceneTypeCreate(GameSceneType.ServerSelectionScreen)]
	public sealed class SharedConnectionRedirectionPayloadHandler : GameMessageHandler<SharedConnectionRedirectPayload>, IConnectionRedirectionEventSubscribable
	{
		private IFullCryptoInitializationService<byte[]> CryptoInitializer { get; }

		/// <summary>
		/// Data model for connection details.
		/// </summary>
		private IGameConnectionEndpointDetails ConnectionEndpoint { get; }

		/// <inheritdoc />
		public event EventHandler OnConnectionRedirection;

		/// <inheritdoc />
		public SharedConnectionRedirectionPayloadHandler([NotNull] IGameConnectionEndpointDetails connectionEndpoint, [NotNull] IFullCryptoInitializationService<byte[]> cryptoInitializer, ILog logger) 
			: base(logger)
		{
			ConnectionEndpoint = connectionEndpoint ?? throw new ArgumentNullException(nameof(connectionEndpoint));
			CryptoInitializer = cryptoInitializer ?? throw new ArgumentNullException(nameof(cryptoInitializer));
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, SharedConnectionRedirectPayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Redirecting Login to {BuildLoginDebugString(payload)}");

			//Have to clear crypto since we're connecting to a new endpoint
			CryptoInitializer.DecryptionInitializable.Uninitialize();
			CryptoInitializer.EncryptionInitializable.Uninitialize();

			//We don't immediately redirect in the handler anymore. We initialize the new connection endpoint
			//to a data model and then broadcast that we recieved a redirection request. This way scene loading
			//can happen inbetween.
			ConnectionEndpoint.IpAddress = payload.EndpointAddress.ToString();
			ConnectionEndpoint.Port = payload.EndpointerPort;

			//Dispatch the redirection event.
			OnConnectionRedirection?.Invoke(this, EventArgs.Empty);
		}

		private string BuildLoginDebugString(SharedConnectionRedirectPayload payload)
		{
			return $"Ip: {payload.EndpointAddress} Port: {payload.EndpointerPort}";
		}
	}
}
