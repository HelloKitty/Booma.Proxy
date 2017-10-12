using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class SelectCharacterRequestSender
	{
		/// <summary>
		/// The sending service.
		/// </summary>
		[Inject]
		private IClientPayloadSendService<PSOBBLoginPacketPayloadClient> SendService { get; }

		/// <summary>
		/// Data model that contains the data about the selected character.
		/// </summary>
		[Inject]
		private ICharacterSlotSelectedModel SelectedModel { get; }

		public void SendCharacterSelection()
		{
			//Just send the request
			SendService.SendMessage(new LoginCharacterSelectionRequestPayload((byte)SelectedModel.SlotSelected, CharacterSelectionType.PlaySelection));
		}
	}
}
