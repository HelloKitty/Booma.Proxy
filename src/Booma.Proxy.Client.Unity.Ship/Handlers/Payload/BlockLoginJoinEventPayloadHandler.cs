using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	[Injectee]
	public class BlockLoginJoinEventPayloadHandler : GameMessageHandler<BlockLobbyJoinEventPayload>
	{
		//TODO: Is it ok to reuse this?
		[Inject]
		private ICharacterSlotSelectedModel SlotModel { get; }

		[SerializeField]
		private UnityEvent OnLobbyJoinEvent;

		private IDictionary<int, string> LobbyNumberToSceneNameMap { get; } = new LobbyMapToSceneMappingCollection();

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockLobbyJoinEventPayload payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"**Handling**: {nameof(BlockLobbyJoinEventPayload)}");

			//We need to make sure the lobby scene is registered
			if(!LobbyNumberToSceneNameMap.ContainsKey(payload.LobbyNumber))
			{
				string lobbyError = $"Tried to enter Lobby: {payload.LobbyNumber} but no lobby for that id was registered.";
				if(Logger.IsErrorEnabled)
					Logger.Error(lobbyError);

				throw new InvalidOperationException(lobbyError);
			}

			//Just set the old char slot to the clientid
			//It's basically like a slot, like a lobby or party slot.
			SlotModel.SlotSelected = payload.ClientId;

			OnLobbyJoinEvent?.Invoke();

			//TODO: Handle multiple different lobby scenes
			//Now we need to load the actual lobby
			//Doing so will require us to load a new lobby scene
			SceneManager.LoadSceneAsync(LobbyNumberToSceneNameMap[payload.LobbyNumber]).allowSceneActivation = true;

			//Don't send anything here, the server will send a 0x60 0x6F after this
			return Task.CompletedTask;
		}
	}
}
