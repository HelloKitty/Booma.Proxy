using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class LoginLoginResponsePayloadHandler : LoginMessageHandler<LoginLoginResponsePayload>
	{
		/// <summary>
		/// Repository to load and session the session data.
		/// </summary>
		[Inject]
		private ILoginSessionDetails SessionDetails { get; }

		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBLoginPacketPayloadClient> context, LoginLoginResponsePayload payload)
		{
			//We don't yet handle the UI for this so we just log it
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved LoginResponse: {payload.ResponseCode}");

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Tag: {payload.Tag} GuildCard: {payload.GuildCard} TeamId: {payload.TeamId}");

			//Set the data required to flow through the login process
			SessionDetails.SessionId = payload.TeamId;
			SessionDetails.SessionVerificationData = payload.SecurityData;

			return Task.CompletedTask;
		}
	}
}
