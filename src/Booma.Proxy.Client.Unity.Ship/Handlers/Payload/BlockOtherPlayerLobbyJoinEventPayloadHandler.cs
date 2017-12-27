using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class BlockOtherPlayerLobbyJoinEventPayloadHandler : GameMessageHandler<BlockOtherPlayerJoinedLobbyEventPayload>
	{
		[Inject]
		private INetworkPlayerFactory PlayerFactory { get; }

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
	}
}
