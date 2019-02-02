using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.PreShipSelectionScene)]
	public sealed class CharacterSelectionResultEventListenerInitializable : IGameInitializable
	{
		private IOnCharacterSelectionAcknowledgementEventSubscribable CharacterAckSubscriptionService { get; }

		/// <inheritdoc />
		public CharacterSelectionResultEventListenerInitializable([NotNull] IOnCharacterSelectionAcknowledgementEventSubscribable characterAckSubscriptionService)
		{
			CharacterAckSubscriptionService = characterAckSubscriptionService ?? throw new ArgumentNullException(nameof(characterAckSubscriptionService));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			CharacterAckSubscriptionService.OnCharacterSelectionAcknowledgementRecieved += OnCharacterSelectionAckRecieved;
			return Task.CompletedTask;
		}

		private void OnCharacterSelectionAckRecieved(object sender, [NotNull] CharacterSelectionAckEventArgs e)
		{
			if(e == null) throw new ArgumentNullException(nameof(e));

			switch(e.AckType)
			{
				case CharacterSelectionAckType.BB_CHAR_ACK_UPDATE:
					break;
				case CharacterSelectionAckType.BB_CHAR_ACK_SELECT:
					OnSelectionSuccessful();
					break;
				case CharacterSelectionAckType.BB_CHAR_ACK_NONEXISTANT:
					break;
			}
		}

		private void OnSelectionSuccessful()
		{
			//The character selected was successfully selected
			//so we should load the next scene.
			//TODO: We should not hardcode scene id NOR should we directly do scene loading. Do it through a service

			//We can just load right away.
			SceneManager.LoadSceneAsync(3).allowSceneActivation = true;
		}
	}
}