using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace Booma.Proxy
{
	[PSOBBHandler]
	public sealed class SharedCreateMessageBoxEventPayloadHandler : BasePSOBBIncomingInteropPayloadHandler<SharedCreateMessageBoxEventPayload>
	{
		/// <inheritdoc />
		public SharedCreateMessageBoxEventPayloadHandler(ILog logger)
			: base(logger)
		{

		}

		public override Task HandleMessage(InteropPSOBBPeerMessageContext context, SharedCreateMessageBoxEventPayload payload)
		{
			//We don't yet handle the UI for this so we just log it
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved MessageBox: {payload.Message}");

			return Task.CompletedTask;
		}
	}
}
