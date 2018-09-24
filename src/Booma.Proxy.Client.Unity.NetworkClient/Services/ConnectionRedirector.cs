using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;
using UnityEngine;
using Unitysync.Async;

namespace Booma.Proxy
{
	/// <summary>
	/// Redirects the connection to the connection
	/// stored in the connection details model.
	/// </summary>
	[Injectee]
	public sealed class ConnectionRedirector : MonoBehaviour
	{
		[Inject]
		private IGameObjectFactory GameObjectFactory { get; }

		/// <summary>
		/// The connection/reconnection/redirection service.
		/// </summary>
		[Inject]
		private IConnectionService ConnectionService { get; }


		[Inject]
		private IFullCryptoInitializationService<byte[]> CryptoInitializer { get; }

		[SerializeField]
		private GameObject NetworkClientPrefab;

		/// <summary>
		/// Redirects the connection to the details in
		/// the <see cref="ConnectionDetails"/> model.
		/// </summary>
		public void Redirect()
		{
			//absolutely critical that the encryption be uninitialaized
			CryptoInitializer.DecryptionInitializable.Uninitialize();
			CryptoInitializer.EncryptionInitializable.Uninitialize();

			//We actually DO want to block everything until we connect.
			ConnectionService.Disconnect();

			//Just create a new client
			GameObjectFactory.Create(NetworkClientPrefab)
				.GetComponent<GameNetworkClient>()
				.StartConnection();
		}
	}
}
