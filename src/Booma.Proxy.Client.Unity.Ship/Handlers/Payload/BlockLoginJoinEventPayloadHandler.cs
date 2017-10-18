using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class BlockLoginJoinEventPayloadHandler : GameMessageHandler<BlockLobbyJoinEventPayload>
	{
		//TODO: Is it ok to reuse this?
		[Inject]
		private ICharacterSlotSelectedModel SlotModel { get; }

		[SerializeField]
		private UnityEvent OnLobbyJoinEvent;

		[SerializeField]
		private int TestLobbyScene;

		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, BlockLobbyJoinEventPayload payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"**Handling**: {nameof(BlockLobbyJoinEventPayload)}");

			//Just set the old char slot to the clientid
			//It's basically like a slot, like a lobby or party slot.
			SlotModel.SlotSelected = payload.ClientId;

			OnLobbyJoinEvent?.Invoke();

			//TODO: Handle multiple different lobby scenes
			//Now we need to load the actual lobby
			//Doing so will require us to load a new lobby scene
			SceneManager.LoadSceneAsync(TestLobbyScene).allowSceneActivation = true;

			//Don't send anything here, the server will send a 0x60 0x6F after this
			return Task.CompletedTask;
		}
	}
}
