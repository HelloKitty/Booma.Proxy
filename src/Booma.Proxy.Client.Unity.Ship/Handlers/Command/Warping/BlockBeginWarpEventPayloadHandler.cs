using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// The handler for <see cref="Sub60ClientWarpBeginEventCommand"/> which handles this payload
	/// alerting 
	/// </summary>
	[AdditionalRegisterationAs(typeof(IWarpBeginEventSubscribable))]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class BlockBeginWarpEventPayloadHandler : Command60Handler<Sub60ClientWarpBeginEventCommand>, IWarpBeginEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler OnWarpBeginning;

		/// <inheritdoc />
		public BlockBeginWarpEventPayloadHandler(ILog logger)
			: base(logger)
		{

		}

		/// <inheritdoc />
		protected override async Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60ClientWarpBeginEventCommand payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved: {this.MessageName()}.");

			//TODO: We should really be initializing the quest data, or whatever it is, this packet sends.
			OnWarpBeginning?.Invoke(this, EventArgs.Empty);

			/*INetworkPlayer player = null;
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
			}*/

			/*//TODO: Send rotation
			//TODO: What should the W coord be? How sould we handle this poition?
			//We can't do anything with the data right now
			await context.PayloadSendService.SendMessage(new Sub60TeleportToPositionCommand((byte)player.Identity.EntityId,
				ScalerService.UnScale(player.Transform.Position).ToNetworkVector3()).ToPayload());

			//Now we have to send a 1F to start the warp
			//Tell the server we're warping now
			await context.PayloadSendService.SendMessage(new Sub60WarpToNewAreaCommand((byte)player.Identity.EntityId, ZoneId).ToPayload());

			//TODO: Should we send ClientId with this one too?
			//We can just send a finished right away, we have nothing to load really
			await context.PayloadSendService.SendMessage(new Sub60FinishedWarpingBurstingCommand().ToPayload());*/
		}
	}
}
