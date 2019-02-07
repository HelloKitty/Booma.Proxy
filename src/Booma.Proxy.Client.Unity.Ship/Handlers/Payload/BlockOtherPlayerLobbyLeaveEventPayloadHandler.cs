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
	//Can leave anywhere in lobby or in game technically
	[AdditionalRegisterationAs(typeof(IRemotePlayerLeaveLobbyEventSubscribable))]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class BlockOtherPlayerLobbyLeaveEventPayloadHandler : GameMessageHandler<BlockOtherPlayerLeaveLobbyEventPayload>, IRemotePlayerLeaveLobbyEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler<RemotePlayerLeaveLobbyEventArgs> OnRemotePlayerLeftLobby;

		/// <inheritdoc />
		public BlockOtherPlayerLobbyLeaveEventPayloadHandler([NotNull] INetworkEntityRegistery<INetworkPlayer> playerRegistry, ILog logger) 
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockOtherPlayerLeaveLobbyEventPayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Player with Id: {payload.ClientId} left Lobby. CurrentLeader: {payload.LeaderId}");

			//TODO: So, we don't really want to do this INPLACE. But we're kinda forced to remove players INPLACE. Because of Sega's index/id design
			//we can't queue it up to be handled at another time becuase a new player WITH THAT ID, might join. So dumb but can't be avoided.
			OnRemotePlayerLeftLobby?.Invoke(this, new RemotePlayerLeaveLobbyEventArgs(EntityGuid.ComputeEntityGuid(EntityType.Player, payload.ClientId)));

			return Task.CompletedTask;
		}
	}
}
