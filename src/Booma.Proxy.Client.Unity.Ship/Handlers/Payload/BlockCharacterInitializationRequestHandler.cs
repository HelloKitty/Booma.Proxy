using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	//TODO: Do we need this on Pioneer2?
	[SceneTypeCreate(GameSceneType.LobbyDefault)] //lobby has this handler too, according to my old self.
	[SceneTypeCreate(GameSceneType.PreBlockBurstingScene)]
	//[SceneTypeCreate(GameSceneType.Pioneer2)] //Don't know why this has to happen all the time
	public sealed class BlockCharacterInitializationRequestHandler : GameMessageHandler<BlockCharacterDataInitializationServerRequestPayload>
	{
		/// <inheritdoc />
		public BlockCharacterInitializationRequestHandler(ILog logger) 
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockCharacterDataInitializationServerRequestPayload payload)
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
