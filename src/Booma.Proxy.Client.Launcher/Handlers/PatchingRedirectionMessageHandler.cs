using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class PatchingRedirectionMessageHandler : IClientPayloadSpecificMessageHandler<PatchingRedirectPayload, PSOBBPatchPacketPayloadClient>
	{
		/// <inheritdoc />
		public async Task HandleMessage(IClientMessageContext<PSOBBPatchPacketPayloadClient> context, PatchingRedirectPayload payload)
		{
			//This payload indicates we need to connect to another endpoint.
			//In this case it'll be the patch service that provides patching files
			await context.ConnectionService.ConnectAsync(new IPAddress(payload.IPAddress), payload.Port);
		}
	}
}
