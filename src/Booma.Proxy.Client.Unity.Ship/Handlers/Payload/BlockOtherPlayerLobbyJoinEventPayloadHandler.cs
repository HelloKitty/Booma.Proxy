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
	//TODO: Rewrite
	/*[SceneTypeCreate(GameSceneType.RagolDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class BlockOtherPlayerLobbyJoinEventPayloadHandler : GameMessageHandler<BlockOtherPlayerJoinedLobbyEventPayload>
	{
		private INetworkPlayerFactory PlayerFactory { get; }

		/// <inheritdoc />
		public BlockOtherPlayerLobbyJoinEventPayloadHandler([NotNull] INetworkPlayerFactory playerFactory, ILog logger) 
			: base(logger)
		{
			PlayerFactory = playerFactory ?? throw new ArgumentNullException(nameof(playerFactory));
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockOtherPlayerJoinedLobbyEventPayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Player Lobby Join: {payload.ClientId} LeaderId: {payload.LeaderId} EventId: {payload.EventId} Lobby: {payload.LobbyNumber}");

			//Create the joined player
			//We don't really have a position for them though, it didn't come in this packet
			PlayerFactory.CreateEntity(payload.ClientId);

			return Task.CompletedTask;
		}
	}*/
}
