using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class LoginShipSelectionMarqueeHandler : LoginMessageHandler<LoginMarqueeScrollChangeEventPayload>
	{
		//TODO: This is just for testing purposes
		[SerializeField]
		public UnityEngine.UI.Text TempMarqueeText;

		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBLoginPacketPayloadClient> context, LoginMarqueeScrollChangeEventPayload payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug(payload.Message.Replace("Destiny", @"[redacted]").Replace("playpso", "[redacted]"));

			//TODO: This is the scrolling marquee at the top of the ship. When the UI is implemented we should handle it
			TempMarqueeText.text = payload.Message;

			return Task.CompletedTask;
		}
	}
}
