using System;
using System.Collections.Generic;
using System.Text;
using GladMMO;
using GladNet;

namespace Booma.Proxy
{
	[SceneTypeCreateGladMMO(GladMMO.GameSceneType.InstanceServerScene)]
	public sealed class SendPSOBBChatMessageEventListener : Glader.Essentials.BaseSingleEventListenerInitializable<IChatTextMessageEnteredEventSubscribable, ChatTextMessageEnteredEventArgs>
	{
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		public SendPSOBBChatMessageEventListener(IChatTextMessageEnteredEventSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		protected override void OnEventFired(object source, ChatTextMessageEnteredEventArgs args)
		{
			//TODO: Handle channels differently.
			SendService.SendMessage(new BlockTextChatMessageRequestPayload(args.Content));
		}
	}
}
