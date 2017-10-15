using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class BlockCharacterInitializationRequestHandler : GameMessageHandler<BlockCharacterDataInitializationServerRequestPayload>
	{
		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, BlockCharacterDataInitializationServerRequestPayload payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"**Handling**: {nameof(BlockCharacterDataInitializationServerRequestPayload)}");

			//This is dumb, the server is asking us for character data when it just sent it.
			//Why? Sega please... But we just send nothing since no SANE developer should trust this data
			context.PayloadSendService.SendMessage(new BlockCharacterDataInitializeClientResponsePayload());

			return Task.CompletedTask;
		}
	}
}
