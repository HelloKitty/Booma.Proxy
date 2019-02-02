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
	public sealed class CharacterLoginResponseEventInitializable : IGameInitializable
	{
		private ILoginResponseEventSubscribable LoginResponseSubscriptionService { get; }

		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		/// <inheritdoc />
		public CharacterLoginResponseEventInitializable([NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService, [NotNull] ILoginResponseEventSubscribable loginResponseSubscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			LoginResponseSubscriptionService = loginResponseSubscriptionService ?? throw new ArgumentNullException(nameof(loginResponseSubscriptionService));
		}

		/// <inheritdoc />
		protected void OnLoginSuccess(object sender, LoginResultEventArgs args)
		{
			//The shared login handler on the character
			//server needs to send
			//Send the request for all
			for(int i = 0; i < 4; i++) //TODO: Don't hardcode character limit.
				SendService.SendMessage(new CharacterCharacterSelectionRequestPayload((byte)i, CharacterSelectionType.Preview));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			LoginResponseSubscriptionService.OnLoginProcessResult += OnLoginSuccess;
			return Task.CompletedTask;
		}
	}
}
