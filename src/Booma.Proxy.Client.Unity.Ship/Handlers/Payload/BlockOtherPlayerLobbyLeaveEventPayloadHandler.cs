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
	public sealed class BlockOtherPlayerLobbyLeaveEventPayloadHandler : GameMessageHandler<BlockOtherPlayerLeaveLobbyEventPayload>
	{
		private INetworkEntityRegistery<INetworkPlayer> PlayerRegistry { get; }

		/// <inheritdoc />
		public BlockOtherPlayerLobbyLeaveEventPayloadHandler([NotNull] INetworkEntityRegistery<INetworkPlayer> playerRegistry, ILog logger) 
			: base(logger)
		{
			PlayerRegistry = playerRegistry ?? throw new ArgumentNullException(nameof(playerRegistry));
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockOtherPlayerLeaveLobbyEventPayload payload)
		{
			//TODO: We can't check that we have this spawned, so we should address that.
			INetworkPlayer player = PlayerRegistry.RemoveEntity(payload.ClientId);

			if(player == null)
			{
				Logger.Warn($"Recieved LobbyLeave for unknown Client: {payload.ClientId}.");
				return Task.CompletedTask;
			}

			player.Despawn();

			return Task.CompletedTask;
		}
	}
}
