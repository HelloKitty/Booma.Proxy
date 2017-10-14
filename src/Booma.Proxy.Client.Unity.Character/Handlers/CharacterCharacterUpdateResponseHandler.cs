using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

namespace Booma.Proxy
{
	//TODO: This is a test handler. It should not be used in the final product
	[Injectee]
	public sealed class CharacterCharacterUpdateResponseHandler : GameMessageHandler<CharacterCharacterUpdateResponsePayload>
	{
		[Serializable]
		public struct CharacterTabUIElement
		{
			[DrawWithUnity]
			[SerializeField]
			public UnityEngine.UI.Button ButtonElement;

			[SerializeField]
			public UnityEngine.UI.Text TextElement;
		}

		//TODO: We should have a better MVC, or something, approach once the UI is finalized
		[SerializeField]
		public List<CharacterTabUIElement> Elements;

		[Inject]
		private ICharacterSlotSelectedModel SelectedSlotModel;

		[SerializeField]
		private UnityEvent OnCharacterSelected;

		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, CharacterCharacterUpdateResponsePayload payload)
		{
			if(Elements == null || Elements.Count < payload.SlotSelected)
				throw new InvalidOperationException($"Character slot unavailable for Slot: {payload.SlotSelected}");

			Elements[payload.SlotSelected].TextElement.text = payload.CharacterData.CharacterName.Replace("\tE", "");
			Elements[payload.SlotSelected].ButtonElement.onClick.AddListener(() =>
			{
				//Save the character we picked
				SelectedSlotModel.SlotSelected = payload.SlotSelected;
				OnCharacterSelected?.Invoke();
			});

			return Task.CompletedTask;
		}
	}
}
