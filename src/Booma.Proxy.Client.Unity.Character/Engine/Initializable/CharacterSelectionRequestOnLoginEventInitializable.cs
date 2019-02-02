using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.PreShipSelectionScene)]
	public class CharacterSelectionRequestOnLoginEventInitializable : IGameInitializable
	{
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		private ILog Logger { get; }

		private ILoginResponseEventSubscribable LoginEventSubscriptionService { get; }

		private ICharacterSlotSelectedModel SlotModel { get; }

		/// <inheritdoc />
		public CharacterSelectionRequestOnLoginEventInitializable([NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService, [NotNull] ILog logger, [NotNull] ILoginResponseEventSubscribable loginEventSubscriptionService, [NotNull] ICharacterSlotSelectedModel slotModel)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			LoginEventSubscriptionService = loginEventSubscriptionService ?? throw new ArgumentNullException(nameof(loginEventSubscriptionService));
			SlotModel = slotModel ?? throw new ArgumentNullException(nameof(slotModel));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			//We have to send when we recieve the event, not on init
			LoginEventSubscriptionService.OnLoginSuccess += LoginEventSubscriptionServiceOnOnLoginSuccess;
			return Task.CompletedTask;
		}

		private void LoginEventSubscriptionServiceOnOnLoginSuccess(object sender, EventArgs e)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"OnLogin: Sending {nameof(CharacterCharacterSelectionRequestPayload)} with Id: {SlotModel.SlotSelected}");

			SendService.SendMessage(new CharacterCharacterSelectionRequestPayload(SlotModel.SlotSelected, CharacterSelectionType.PlaySelection))
				.ConfigureAwait(false);
		}
	}
}
