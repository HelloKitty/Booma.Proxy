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
		}
	}
}
