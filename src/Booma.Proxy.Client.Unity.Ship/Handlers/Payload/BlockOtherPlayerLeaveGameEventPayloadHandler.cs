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
	[AdditionalRegisterationAs(typeof(IRemotePlayerLeaveLobbyEventSubscribable))]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	public sealed class BlockOtherPlayerLeaveGameEventPayloadHandler : GameMessageHandler<BlockOtherPlayerLeaveGameEventPayload>, IRemotePlayerLeaveLobbyEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler<RemotePlayerLeaveLobbyEventArgs> OnRemotePlayerLeftLobby;

		/// <inheritdoc />
		public BlockOtherPlayerLeaveGameEventPayloadHandler([NotNull] INetworkEntityRegistery<INetworkPlayer> playerRegistry, ILog logger) 
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockOtherPlayerLeaveGameEventPayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Warn($"Recieved Player GameLeave From EntityId: {payload.Identifier}.");

			//We should just broadcast that a player left the lobby.
			OnRemotePlayerLeftLobby?.Invoke(this, new RemotePlayerLeaveLobbyEventArgs(EntityGuid.ComputeEntityGuid(EntityType.Player, payload.Identifier)));

			return Task.CompletedTask;
		}
	}
}
