using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace Booma.Proxy
{
	/// <summary>
	/// The ping event handler. Will just send the ping response.
	/// </summary>
	public class BlockPingEventHandler : GameMessageHandler<BlockClientPingEventPayload>
	{
		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockClientPingEventPayload payload)
		{
			//Just send the ping response; otherwise we'll be disconnected.
			context.PayloadSendService.SendMessage(new BlockClientPingResponsePayload());

			return Task.CompletedTask;
		}
	}
}
