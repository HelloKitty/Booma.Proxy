using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	/// <summary>
	/// The ping event handler. Will just send the ping response.
	/// </summary>
	[PSOBBHandler]
	public class InteropBlockPingEventHandler : BasePSOBBIncomingInteropPayloadHandler<BlockClientPingEventPayload>
	{
		/// <inheritdoc />
		public InteropBlockPingEventHandler(ILog logger)
			: base(logger)
		{

		}

		public override Task HandleMessage(InteropPSOBBPeerMessageContext context, BlockClientPingEventPayload payload)
		{
			//Just send the ping response; otherwise we'll be disconnected.
			context.PayloadSendService.SendMessageImmediately(new BlockClientPingResponsePayload());

			return Task.CompletedTask;
		}
	}
}