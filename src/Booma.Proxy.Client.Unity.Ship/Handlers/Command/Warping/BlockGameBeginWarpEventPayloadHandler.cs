using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;

namespace Booma.Proxy
{
	/// <summary>
	/// Command handler that handles the game/62 version of the <see cref="Sub60ClientWarpBeginEventCommand"/>.
	/// This one uses a similar one called <see cref="Sub62ClientWarpBeginEventCommand"/> for games
	/// </summary>
	[Injectee]
	public sealed class BlockGameBeginWarpEventPayloadHandler : Command62Handler<Sub62ClientWarpBeginEventCommand>
	{
		[Inject]
		private INetworkPlayerFactory PlayerFactory { get; }

		[Inject]
		private IUnitScalerStrategy ScalerService { get; }

		/// <inheritdoc />
		public BlockGameBeginWarpEventPayloadHandler(ILog logger) 
			: base(logger)
		{

		}

		protected override async Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub62ClientWarpBeginEventCommand command)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved: {this.MessageName()} about to create local player.");

			INetworkPlayer player = null;
			try
			{
				//TODO: Is this where we should do this?
				//We need to create the player represenation here
				player = PlayerFactory.CreateLocalPlayer();
			}
			catch(Exception e)
			{
				if(Logger.IsErrorEnabled || Logger.IsFatalEnabled)
					Logger.Fatal($"Failed to create network player. Exception: {e.Message} \n\n Stacktrace: {e.StackTrace}");
				throw;
			}

			//TODO: Handle the zoneid better
			await context.PayloadSendService.SendMessage(new Sub60TeleportToPositionCommand(player.Identity.EntityId, ScalerService.UnScale(player.Transform.Position).ToNetworkVector3()).ToPayload());
			await context.PayloadSendService.SendMessage(new Sub60WarpToNewAreaCommand(player.Identity.EntityId, 0).ToPayload()); //pioneer 2
			await context.PayloadSendService.SendMessage(new Sub60FinishedMapLoadCommand(player.Identity.EntityId).ToPayload());

			//This tells everyone else we're doing bursting
			await context.PayloadSendService.SendMessage(new BlockFinishedGameBurstingRequestPayload());
		}
	}
}
