using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Proxy
{
	/// <summary>
	/// Handler that handles the <see cref="SharedLoginResponsePayload"/> and initializes the recieved payload
	/// information.
	/// </summary>
	[Injectee]
	public sealed class SharedLoginResponsePayloadHandler : GameMessageHandler<SharedLoginResponsePayload>
	{
		/// <summary>
		/// Repository to load and session the session data.
		/// </summary>
		[Inject]
		private IClientSessionDetails SessionDetails { get; }

		//TODO: Find out why Odin doesn't like private UnityAction
		/// <summary>
		/// Event dispatched when the login failed.
		/// </summary>
		[SerializeField]
		private UnityEvent OnLoginFailed;

		//TODO: Find out why Odin doesn't like private UnityAction
		/// <summary>
		/// Event dispatched when the login was successful.
		/// </summary>
		[SerializeField]
		private UnityEvent OnLoginSuccess;

		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, SharedLoginResponsePayload payload)
		{
			//We don't yet handle the UI for this so we just log it
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved LoginResponse: {payload.ResponseCode}");

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Tag: {payload.Tag} GuildCard: {payload.GuildCard} TeamId: {payload.TeamId}");

			if(!payload.isSuccessful)
			{
				OnLoginFailed?.Invoke();
				return Task.CompletedTask;
			}	

			//Set the data required to flow through the login process
			SessionDetails.SessionId = payload.TeamId;
			SessionDetails.SessionVerificationData = payload.SecurityData;
			SessionDetails.GuildCardNumber = payload.GuildCard;

			//Invoke login success if it's succesful at this point.
			OnLoginSuccess?.Invoke();

			return Task.CompletedTask;
		}
	}
}
