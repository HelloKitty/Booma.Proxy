using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	//TODO: Do we need this on Pioneer2?
	[PSOBBHandler]
	public sealed class InteropCharacterInitializationRequestHandler : BasePSOBBIncomingInteropPayloadHandler<BlockCharacterDataInitializationServerRequestPayload>
	{
		/// <inheritdoc />
		public InteropCharacterInitializationRequestHandler(ILog logger)
			: base(logger)
		{

		}

		public override Task HandleMessage(InteropPSOBBPeerMessageContext context, BlockCharacterDataInitializationServerRequestPayload payload)
		{
			//This is dumb, the server is asking us for character data when it just sent it.
			//Why? Sega please... But we just send nothing since no SANE developer should trust this data
			context.PayloadSendService.SendMessage(new BlockCharacterDataInitializeClientResponsePayload());

			return Task.CompletedTask;
		}
	}
}
