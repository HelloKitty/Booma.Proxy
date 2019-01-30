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
	[NetworkMessageHandler(GameSceneType.ServerSelectionScreen)]
	[NetworkMessageHandler(GameSceneType.TitleScreen)] //titlescreen obviously needs to recieve login responses.
	public class SharedLoginResponsePayloadHandler : GameMessageHandler<SharedLoginResponsePayload>
	{
		/// <summary>
		/// Repository to load and session the session data.
		/// </summary>
		private IClientSessionDetails SessionDetails { get; }

		/// <inheritdoc />
		public SharedLoginResponsePayloadHandler([NotNull] IClientSessionDetails sessionDetails, ILog logger)
			: base(logger)
		{
			SessionDetails = sessionDetails ?? throw new ArgumentNullException(nameof(sessionDetails));
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, SharedLoginResponsePayload payload)
		{
			//We don't yet handle the UI for this so we just log it
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved LoginResponse: {payload.ResponseCode}");

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Tag: {payload.Tag} GuildCard: {payload.GuildCard} TeamId: {payload.TeamId}");

			if(!payload.isSuccessful)
			{
				OnLoginFailed();
				return Task.CompletedTask;
			}

			//Set the data required to flow through the login process
			SessionDetails.SessionId = payload.TeamId;
			SessionDetails.SessionVerificationData = payload.SecurityData;
			SessionDetails.GuildCardNumber = payload.GuildCard;

			//Invoke login success if it's succesful at this point.
			OnLoginSuccess();

			return Task.CompletedTask;
		}

		protected virtual void OnLoginFailed()
		{
			
		}

		protected virtual void OnLoginSuccess()
		{

		}
	}
}
