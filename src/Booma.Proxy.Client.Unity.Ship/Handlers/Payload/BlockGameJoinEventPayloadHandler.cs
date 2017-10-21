using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class BlockGameJoinEventPayloadHandler : GameMessageHandler<BlockGameJoinEventPayload>
	{
		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, BlockGameJoinEventPayload payload)
		{
			if(Logger.IsInfoEnabled)
			{
				Logger.Info($"Assigned Id: {payload.Identifier}");
				Logger.Info($"Players in room: {payload.Players.Count()}");
				foreach(var p in payload.Players)
					Logger.Info($"Player: {p}");

				Logger.Info($"{payload.Settings}");
			}

			return Task.CompletedTask;
		}
	}
}
