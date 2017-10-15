using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class BlockRunningPositionChangedEventPayloadHandler : Command60Handler<Sub60RunningPositionChangedEvent>
	{
		/// <inheritdoc />
		protected override Task HandleCommand(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60RunningPositionChangedEvent payload)
		{
			//We only log this at the moment
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Client: {payload.ClientId} Pos: X: {payload.Position.X} Z: {payload.Position.Y}");

			return Task.CompletedTask;
		}
	}
}
