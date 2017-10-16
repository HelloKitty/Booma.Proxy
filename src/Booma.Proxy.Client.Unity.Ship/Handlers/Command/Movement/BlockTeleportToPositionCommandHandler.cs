using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class BlockTeleportToPositionCommandHandler : Command60Handler<Sub60TeleportToPositionCommand>
	{
		/// <inheritdoc />
		protected override Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60TeleportToPositionCommand command)
		{
			//TODO: Don't do anything with this
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved {nameof(Sub60TeleportToPositionCommand)} ClientId: {command.ClientId} X: {command.Position.X} Y: {command.Position.Y} Z: {command.Position.Z}");

			return Task.CompletedTask;
		}
	}
}
