using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class SharedCreateMessageBoxEventPayloadHandler : GameMessageHandler<LoginCreateMessageBoxEventPayload>
	{
		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, LoginCreateMessageBoxEventPayload payload)
		{
			//We don't yet handle the UI for this so we just log it
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved MessageBox: {payload.Message}");

			return Task.CompletedTask;
		}
	}
}
