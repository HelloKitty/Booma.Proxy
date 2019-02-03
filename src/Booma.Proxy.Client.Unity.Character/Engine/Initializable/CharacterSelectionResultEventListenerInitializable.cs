using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.PreShipSelectionScene)]
	public sealed class CharacterSelectionResultEventListenerInitializable : BaseSingleEventListenerInitializable<IOnCharacterSelectionAcknowledgementEventSubscribable, CharacterSelectionAckEventArgs>
	{
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		/// <inheritdoc />
		public CharacterSelectionResultEventListenerInitializable([NotNull] IOnCharacterSelectionAcknowledgementEventSubscribable characterAckSubscriptionService, [NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService)
			: base(characterAckSubscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		/// <inheritdoc />
		protected override async void OnEventFired(object source, CharacterSelectionAckEventArgs args)
		{
			if(args == null) throw new ArgumentNullException(nameof(args));

			switch(args.AckType)
			{
				case CharacterSelectionAckType.BB_CHAR_ACK_UPDATE:
					break;
				case CharacterSelectionAckType.BB_CHAR_ACK_SELECT:
					await OnSelectionSuccessful();
					break;
				case CharacterSelectionAckType.BB_CHAR_ACK_NONEXISTANT:
					break;
			}
		}

		private async Task OnSelectionSuccessful()
		{
			//The character selected was successfully selected
			//so we should load the next scene.
			//TODO: We should not hardcode scene id NOR should we directly do scene loading. Do it through a service

			//We should disconnect first
			//Client sends 05 here, can cause issues if we don't maybe?
			await SendService.SendMessage(new SharedDisconnectionRequestPayload());

			//We can just load right away.
			SceneManager.LoadSceneAsync(3).allowSceneActivation = true;
		}
	}
}