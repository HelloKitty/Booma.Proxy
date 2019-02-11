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
	/// The handler for <see cref="Sub60ClientBurstBeginEventCommand"/> which handles this payload
	/// alerting 
	/// </summary>
	[AdditionalRegisterationAs(typeof(IWarpBeginEventSubscribable))]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class Sub60ClientBurstBeginEventCommandHandler : Command60Handler<Sub60ClientBurstBeginEventCommand>, IWarpBeginEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler OnBurstBeginning;

		/// <inheritdoc />
		public Sub60ClientBurstBeginEventCommandHandler(ILog logger)
			: base(logger)
		{

		}

		/// <inheritdoc />
		protected override async Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60ClientBurstBeginEventCommand payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved: {this.MessageName()}.");

			//TODO: We should really be initializing the quest data, or whatever it is, this packet sends.
			OnBurstBeginning?.Invoke(this, EventArgs.Empty);
		}
	}
}
