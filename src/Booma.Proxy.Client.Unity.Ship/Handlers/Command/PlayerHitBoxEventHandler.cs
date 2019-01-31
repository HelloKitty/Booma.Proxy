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
	[SceneTypeCreate(GameSceneType.Pioneer2)] //the reason we need to recieve this on Pioneer 2 is so we can adjust the gamestate here.
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	public sealed class PlayerHitBoxEventHandler : Command60Handler<Sub60ClientBoxHitEventCommand>
	{
		/// <inheritdoc />
		public PlayerHitBoxEventHandler(ILog logger) 
			: base(logger)
		{
			
		}

		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60ClientBoxHitEventCommand command)
		{
			if(this.Logger.IsDebugEnabled)
				Logger.Debug($"Encountered BoxHit: {command}");

			//TODO: What should we do?

			return Task.CompletedTask;
		}
	}
}
