using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class ChatMessageRequestSender : MonoBehaviour
	{
		[Inject]
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		/// <summary>
		/// A settable message string that can be sent by invoking the request
		/// send message.
		/// </summary>
		public string Message { get; set; }

		public void SendChatMessage(string message)
		{
			if(string.IsNullOrEmpty(message))
				return;

			SendService.SendMessage(new BlockTextChatMessageRequestPayload(message));
		}

		public void SendChatMessage()
		{
			SendChatMessage(Message);
		}
	}
}
