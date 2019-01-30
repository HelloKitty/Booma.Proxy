using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	//TODO: This is a test handler. It should not be used in the final product
	[NetworkMessageHandler(GameSceneType.CharacterSelectionScreen)]
	public sealed class CharacterCharacterUpdateResponseHandler : GameMessageHandler<CharacterCharacterUpdateResponsePayload>
	{
		private ICharacterSlotSelectedModel SelectedSlotModel { get; }

		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		private CharacterTabUIElementsContext CharacterScreenUIContext { get; }

		/// <inheritdoc />
		public CharacterCharacterUpdateResponseHandler([NotNull] ICharacterSlotSelectedModel selectedSlotModel, ILog logger, [NotNull] CharacterTabUIElementsContext characterScreenUiContext, [NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService)
			: base(logger)
		{
			SelectedSlotModel = selectedSlotModel ?? throw new ArgumentNullException(nameof(selectedSlotModel));
			CharacterScreenUIContext = characterScreenUiContext ?? throw new ArgumentNullException(nameof(characterScreenUiContext));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, CharacterCharacterUpdateResponsePayload payload)
		{
			if(CharacterScreenUIContext.Elements.Count < payload.SlotSelected)
				throw new InvalidOperationException($"Character slot unavailable for Slot: {payload.SlotSelected}");

			CharacterScreenUIContext.Elements.ElementAt(payload.SlotSelected).ButtonElement.IsInteractable = true;
			CharacterScreenUIContext.Elements.ElementAt(payload.SlotSelected).TextElement.Text = payload.CharacterData.CharacterName.Replace("\tE", "");
			CharacterScreenUIContext.Elements.ElementAt(payload.SlotSelected).ButtonElement.AddOnClickListenerAsync(async () =>
			{
				//Save the character we picked
				SelectedSlotModel.SlotSelected = payload.SlotSelected;
				
				//Disable all character buttons
				foreach(var uiEle in CharacterScreenUIContext.Elements)
					uiEle.ButtonElement.IsInteractable = false;

				//TODO: What is this?
				//I really don't know what this is
				await SendService.SendMessage(new CharacterChecksumRequestPayload(0))
					.ConfigureAwait(false);

				//This starts the long drawnout bullshit for guild card data reading or whatever
				await SendService.SendMessage(new CharacterGuildHeaderRequestPayload())
					.ConfigureAwait(false);
			});

			return Task.CompletedTask;
		}
	}
}
