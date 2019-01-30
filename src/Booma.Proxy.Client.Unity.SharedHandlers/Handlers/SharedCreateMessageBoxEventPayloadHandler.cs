using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.PreBlockBurstingScene)]
	[SceneTypeCreate(GameSceneType.ServerSelectionScreen)]
	[SceneTypeCreate(GameSceneType.CharacterSelectionScreen)]
	[SceneTypeCreate(GameSceneType.TitleScreen)]
	public sealed class SharedCreateMessageBoxEventPayloadHandler : GameMessageHandler<SharedCreateMessageBoxEventPayload>
	{
		/// <inheritdoc />
		public SharedCreateMessageBoxEventPayloadHandler(ILog logger) 
			: base(logger)
		{
			
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, SharedCreateMessageBoxEventPayload payload)
		{
			//We don't yet handle the UI for this so we just log it
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved MessageBox: {payload.Message}");

			return Task.CompletedTask;
		}
	}
}
