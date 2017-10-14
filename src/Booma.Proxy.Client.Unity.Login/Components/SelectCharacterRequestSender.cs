using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class SelectCharacterRequestSender : MonoBehaviour
	{
		/// <summary>
		/// The sending service.
		/// </summary>
		[Inject]
		private IClientPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		/// <summary>
		/// Data model that contains the data about the selected character.
		/// </summary>
		[Inject]
		private ICharacterSlotSelectedModel SelectedModel { get; }

		[Inject]
		private ILog Logger { get; }

		public void SendCharacterSelection()
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Sending CharSelection: {SelectedModel.SlotSelected}");

			//Just send the request
			SendService.SendMessage(new LoginCharacterSelectionRequestPayload((byte)SelectedModel.SlotSelected, CharacterSelectionType.PlaySelection));
		}
	}
}
