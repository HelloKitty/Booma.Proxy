using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace Booma.Proxy
{
	public sealed class PatchingLoginReadyMessageHandler : IPeerPayloadSpecificMessageHandler<PatchingReadyForLoginRequestPayload, PSOBBPatchPacketPayloadClient>
	{
		/// <inheritdoc />
		public async Task HandleMessage(IPeerMessageContext<PSOBBPatchPacketPayloadClient> context, PatchingReadyForLoginRequestPayload payload)
		{
			//Just send the login. It doesn't need to actually be a legit login
			await context.PayloadSendService.SendMessage(new PatchingLoginRequestPayload("booma", "none"));
		}
	}
}
