using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
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
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

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
			SendService.SendMessage(new CharacterCharacterSelectionRequestPayload((byte)SelectedModel.SlotSelected, CharacterSelectionType.PlaySelection));
		}
	}
}
