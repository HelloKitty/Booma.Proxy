using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace Booma.Proxy
{
	//On login response subscriber for character selection screen.
	[SceneTypeCreate(GameSceneType.CharacterSelectionScreen)]
	public sealed class CharacterLoginResponseEventInitializable : BaseSingleEventListenerInitializable<ILoginResponseEventSubscribable, LoginResultEventArgs>
	{
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		/// <inheritdoc />
		public CharacterLoginResponseEventInitializable(ILoginResponseEventSubscribable subscriptionService, [NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, LoginResultEventArgs args)
		{
			//Not interested if we failed
			if(!args.isSuccessful)
				return;

			//The shared login handler on the character
			//server needs to send
			//Send the request for all
			for(int i = 0; i < 4; i++) //TODO: Don't hardcode character limit.
				SendService.SendMessage(new CharacterCharacterSelectionRequestPayload((byte)i, CharacterSelectionType.Preview));
		}
	}
}
