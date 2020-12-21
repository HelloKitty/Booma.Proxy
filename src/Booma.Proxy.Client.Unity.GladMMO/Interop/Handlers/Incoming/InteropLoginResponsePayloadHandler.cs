using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladMMO;
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

		public override async Task HandleMessage(InteropPSOBBPeerMessageContext context, SharedLoginResponsePayload payload)
		{
			//We don't yet handle the UI for this so we just log it
			if(Logger.IsInfoEnabled)
				Logger.Info($"Received LoginResponse: {payload.ResponseCode}");

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Tag: {payload.Tag} GuildCard: {payload.GuildCard} TeamId: {payload.TeamId}");

			//TODO: Better reason code mapping.
			//Just tell gladmmo that the session was successfully claimed here.
			await context.GladMMOClientPayloadReceiver.SendMessage(new ClientSessionClaimResponsePayload(payload.isSuccessful ? ClientSessionClaimResponseCode.Success : ClientSessionClaimResponseCode.SessionUnavailable))
				.ConfigureAwait(false);
		}
	}
}
