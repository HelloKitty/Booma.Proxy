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
	//TODO: This marquee change handler should be able to handle all marquees for all servers
	//TODO: This is a temporary handler, we really need a fully featured version.
	[AdditionalRegisterationAs(typeof(IMarqueeTextChangedEventSubscribable))]
	[SceneTypeCreate(GameSceneType.ServerSelectionScreen)]
	public sealed class SharedScrollMarqueeChangeHandler : GameMessageHandler<SharedMarqueeScrollChangeEventPayload>, IMarqueeTextChangedEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler<MarqueeTextChangedEventArgs> OnMarqueeTextChangedEvent;

		/// <inheritdoc />
		public SharedScrollMarqueeChangeHandler(ILog logger) 
			: base(logger)
		{
			
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, SharedMarqueeScrollChangeEventPayload payload)
		{
			if(payload == null) throw new ArgumentNullException(nameof(payload));

			if(payload.Message == null)
				Logger.Warn($"Encountered empty Marquee message.");

			string message = payload.Message.Replace("Destiny", @"[redacted]").Replace("playpso", "[redacted]");

			if(Logger.IsDebugEnabled)
				Logger.Debug(message);

			OnMarqueeTextChangedEvent?.Invoke(this, new MarqueeTextChangedEventArgs(message));

			return Task.CompletedTask;
		}
	}
}
