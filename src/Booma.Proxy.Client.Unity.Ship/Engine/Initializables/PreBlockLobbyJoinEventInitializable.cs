using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.PreBlockBurstingScene)]
	public sealed class PreBlockLobbyJoinEventInitializable : IGameInitializable
	{
		private INetworkClientExportable ClientExportable { get; }

		private ILog Logger { get; }

		//TODO: This is bad design
		private IDictionary<int, string> LobbyNumberToSceneNameMap { get; } = new LobbyMapToSceneMappingCollection();

		private ILocalPlayerLobbyJoinEventSubscribable LobbyJoinEventSubscriptionService { get; }

		/// <inheritdoc />
		public PreBlockLobbyJoinEventInitializable([NotNull] INetworkClientExportable clientExportable, [NotNull] ILog logger, [NotNull] ILocalPlayerLobbyJoinEventSubscribable lobbyJoinEventSubscriptionService)
		{
			ClientExportable = clientExportable ?? throw new ArgumentNullException(nameof(clientExportable));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			LobbyJoinEventSubscriptionService = lobbyJoinEventSubscriptionService ?? throw new ArgumentNullException(nameof(lobbyJoinEventSubscriptionService));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			LobbyJoinEventSubscriptionService.OnLocalPlayerLobbyJoined += OnJoinedLobby;
			return Task.CompletedTask;
		}

		private void OnJoinedLobby(object sender, LobbyJoinedEventArgs lobbyJoinArgs)
		{
			//We need to make sure the lobby scene is registered
			if(!LobbyNumberToSceneNameMap.ContainsKey(lobbyJoinArgs.LobbyId))
			{
				string lobbyError = $"Tried to enter Lobby: {lobbyJoinArgs.LobbyId} but no lobby for that id was registered.";
				if(Logger.IsErrorEnabled)
					Logger.Error(lobbyError);

				throw new InvalidOperationException(lobbyError);
			}

			ClientExportable.ExportmanagedClient();

			//TODO: Handle multiple different lobby scenes
			//Now we need to load the actual lobby
			//Doing so will require us to load a new lobby scene
			SceneManager.LoadSceneAsync(LobbyNumberToSceneNameMap[lobbyJoinArgs.LobbyId]).allowSceneActivation = true;
		}
	}
}
