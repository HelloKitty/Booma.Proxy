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
	public class CharacterSelectionRequestOnLoginEventInitializable : BaseSingleEventListenerInitializable<ILoginResponseEventSubscribable, LoginResultEventArgs>
	{
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		private ILog Logger { get; }

		private ICharacterSlotSelectedModel SlotModel { get; }

		/// <inheritdoc />
		public CharacterSelectionRequestOnLoginEventInitializable([NotNull] ILoginResponseEventSubscribable subscriptionService, [NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService, [NotNull] ILog logger, [NotNull] ICharacterSlotSelectedModel slotModel) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			SlotModel = slotModel ?? throw new ArgumentNullException(nameof(slotModel));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, LoginResultEventArgs args)
		{
			if(!args.isSuccessful)
				return;

			if(Logger.IsInfoEnabled)
				Logger.Info($"OnLogin: Sending {nameof(CharacterCharacterSelectionRequestPayload)} with Id: {SlotModel.SlotSelected}");

			SendService.SendMessage(new CharacterCharacterSelectionRequestPayload(SlotModel.SlotSelected, CharacterSelectionType.PlaySelection))
				.ConfigureAwait(false);
		}
	}
}
