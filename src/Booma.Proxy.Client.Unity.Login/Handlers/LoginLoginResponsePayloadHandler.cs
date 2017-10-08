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
		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBLoginPacketPayloadClient> context, LoginLoginResponsePayload payload)
		{
			//We don't yet handle the UI for this so we just log it
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved LoginResponse: {payload.ResponseCode}");

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Tag: {payload.Tag} GuildCard: {payload.GuildCard} TeamId: {payload.TeamId}");

			//TODO: We may need to init the recieved data somewhere.
			return Task.CompletedTask;
		}
	}
}
