using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	public sealed class Default6DCommandHandler : Command6DHandler<UnknownSubCommand6DCommand>
	{
		private int recievedCount = 0;

		/// <inheritdoc />
		public Default6DCommandHandler(ILog logger) 
			: base(logger)
		{
		}

		/// <inheritdoc />
		protected override async Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, UnknownSubCommand6DCommand command)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved Unknown: {command}");

			//TODO: We kinda just count how many we've recieved.
			if(recievedCount == 4)
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"About to send: {nameof(BlockFinishedGameBurstingRequestPayload)}");

				//If we've recieved 4 so far this is the 5th one
				//meaning we are done and can probably send
				//the UnFreeze packet to everyone.
				await context.PayloadSendService.SendMessage(new BlockFinishedGameBurstingRequestPayload());
			}

			recievedCount++;
		}
	}
}
