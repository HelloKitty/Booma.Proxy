using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	//This component must exist in the scene for easier initialization.
	public sealed class LobbyPlayerPrefabProvider : MonoBehaviour, INetworkPlayerPrefabProvider
	{
		[SerializeField]
		private GameObject _localPlayerPrefab;

		[SerializeField]
		private GameObject _remotePlayerPrefab;

		/// <inheritdoc />
		public GameObject LocalPlayerPrefab => _localPlayerPrefab;

		/// <inheritdoc />
		public GameObject RemotePlayerPrefab => _remotePlayerPrefab;

		void Awake()
		{
			if(_localPlayerPrefab == null)
				throw new InvalidOperationException($"Must initialize: {nameof(_localPlayerPrefab)} in {GetType().Name}.");

			if(_remotePlayerPrefab == null)
				throw new InvalidOperationException($"Must initialize: {nameof(_remotePlayerPrefab)} in {GetType().Name}.");
		}
	}
}
