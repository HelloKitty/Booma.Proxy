using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class LoginConnectionRedirectionPayloadHandler : LoginMessageHandler<LoginConnectionRedirectPayload>
	{
		[Inject]
		private IFullCryptoInitializationService<byte[]> CryptoInitializer { get; }

		/// <summary>
		/// Broadcasts when a connection redirection is recieved.
		/// </summary>
		[SerializeField]
		private UnityEvent OnConnectionRedirected;

		/// <summary>
		/// Data model for connection details.
		/// </summary>
		[Inject]
		private ILoginConnectionEndpointDetails ConnectionEndpoint { get; }

		/// <inheritdoc />
		public override async Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, LoginConnectionRedirectPayload payload)
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

			OnConnectionRedirected?.Invoke();
		}

		private string BuildLoginDebugString(LoginConnectionRedirectPayload payload)
		{
			return $"Ip: {payload.EndpointAddress} Port: {payload.EndpointerPort}";
		}
	}
}
