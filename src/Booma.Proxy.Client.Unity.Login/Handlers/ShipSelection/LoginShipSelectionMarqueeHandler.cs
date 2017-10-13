using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class LoginShipSelectionMarqueeHandler : LoginMessageHandler<LoginMarqueeScrollChangeEventPayload>
	{
		//TODO: This is just for testing purposes
		[SerializeField]
		public UnityEngine.UI.Text TempMarqueeText;

		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBLoginPacketPayloadClient> context, LoginMarqueeScrollChangeEventPayload payload)
		{
			if(payload == null) throw new ArgumentNullException(nameof(payload));
			if(payload.Message == null)
				Logger.Warn($"Encountered empty Marquee message.");

			string message = payload.Message.Replace("Destiny", @"[redacted]").Replace("playpso", "[redacted]");

			if(Logger.IsDebugEnabled)
				Logger.Debug(message);

			//TODO: This is the scrolling marquee at the top of the ship. When the UI is implemented we should handle it
			TempMarqueeText.text = message;

			return Task.CompletedTask;
		}
	}
}
