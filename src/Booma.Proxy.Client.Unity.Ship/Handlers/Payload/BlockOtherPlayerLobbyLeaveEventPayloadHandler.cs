using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;

namespace Booma.Proxy
{
	public sealed class BlockOtherPlayerLobbyLeaveEventPayloadHandler : GameMessageHandler<BlockOtherPlayerLeaveLobbyEventPayload>
	{
		[Inject]
		private INetworkPlayerRegistery PlayerRegistry { get; }

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockOtherPlayerLeaveLobbyEventPayload payload)
		{
			//TODO: We can't check that we have this spawned, so we should address that.
			INetworkPlayer player = PlayerRegistry.RemovePlayer(payload.ClientId);

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
