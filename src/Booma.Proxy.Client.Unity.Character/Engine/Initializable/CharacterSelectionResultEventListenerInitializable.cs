using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.PreShipSelectionScene)]
	public sealed class CharacterSelectionResultEventListenerInitializable : BaseSingleEventListenerInitializable<IOnCharacterSelectionAcknowledgementEventSubscribable, CharacterSelectionAckEventArgs>
	{
		/// <inheritdoc />
		public CharacterSelectionResultEventListenerInitializable([NotNull] IOnCharacterSelectionAcknowledgementEventSubscribable characterAckSubscriptionService)
			: base(characterAckSubscriptionService)
		{

		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, CharacterSelectionAckEventArgs args)
		{
			if(args == null) throw new ArgumentNullException(nameof(args));

			switch(args.AckType)
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