using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Redirects the connection to the connection
	/// stored in the connection details model.
	/// </summary>
	[Injectee]
	public sealed class ConnectionRedirector : MonoBehaviour
	{
		/// <summary>
		/// The details model.
		/// </summary>
		[Inject]
		private IGameConnectionEndpointDetails ConnectionDetails { get; }

		/// <summary>
		/// The connection/reconnection/redirection service.
		/// </summary>
		[Inject]
		private IConnectionService ConnectionService { get; }

		[Inject]
		private IFullCryptoInitializationService<byte[]> CryptoInitializer { get; }

		/// <summary>
		/// Redirects the connection to the details in
		/// the <see cref="ConnectionDetails"/> model.
		/// </summary>
		public void Redirect()
		{
			//absolutely critical that the encryption be uninitialized
			CryptoInitializer.DecryptionInitializable.Uninitialize();
			CryptoInitializer.EncryptionInitializable.Uninitialize();

			ConnectionService.Connect(ConnectionDetails.IpAddress, ConnectionDetails.Port);
		}
	}
}
