using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Proxy
{
	/// <summary>
	/// Handler that handles the <see cref="SharedLoginResponsePayload"/> and initializes the recieved payload
	/// information.
	/// </summary>
	[PSOBBHandler]
	public sealed class InteropLoginResponsePayloadHandler : BasePSOBBIncomingInteropPayloadHandler<SharedLoginResponsePayload>
	{
		/// <inheritdoc />
		public InteropLoginResponsePayloadHandler(ILog logger)
			: base(logger)
		{

		}

		public override Task HandleMessage(InteropPSOBBPeerMessageContext context, SharedLoginResponsePayload payload)
		{
			//We don't yet handle the UI for this so we just log it
			if(Logger.IsInfoEnabled)
				Logger.Info($"Received LoginResponse: {payload.ResponseCode}");

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Tag: {payload.Tag} GuildCard: {payload.GuildCard} TeamId: {payload.TeamId}");

			return Task.CompletedTask;
		}
	}
}
