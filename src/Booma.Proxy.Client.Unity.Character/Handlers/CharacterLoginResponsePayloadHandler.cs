using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	[NetworkMessageHandler(GameSceneType.CharacterSelectionScreen)]
	public sealed class CharacterLoginResponsePayloadHandler : SharedLoginResponsePayloadHandler
	{
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		/// <inheritdoc />
		public CharacterLoginResponsePayloadHandler(IClientSessionDetails sessionDetails, ILog logger, [NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService) 
			: base(sessionDetails, logger)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		/// <inheritdoc />
		protected override void OnLoginSuccess()
		{
			//The shared login handler on the character
			//server needs to send
			//Send the request for all
			for(int i = 0; i < 4; i++) //TODO: Don't hardcode character limit.
				SendService.SendMessage(new CharacterCharacterSelectionRequestPayload((byte)i, CharacterSelectionType.Preview));
		}
	}
}
