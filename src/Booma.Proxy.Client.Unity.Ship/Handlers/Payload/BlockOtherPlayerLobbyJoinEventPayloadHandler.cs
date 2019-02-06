using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using Guardians;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	//TODO: Rewrite
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class BlockOtherPlayerLobbyJoinEventPayloadHandler : GameMessageHandler<BlockOtherPlayerJoinedLobbyEventPayload>
	{
		/// <inheritdoc />
		public BlockOtherPlayerLobbyJoinEventPayloadHandler(ILog logger)
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockOtherPlayerJoinedLobbyEventPayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Player Lobby Join: {payload.ClientId} LeaderId: {payload.LeaderId} EventId: {payload.EventId} Lobby: {payload.LobbyNumber}");

			//We actually DON'T need to do anything here.
			//Reason being this packet contains pretty much no relevant data.
			//It just says "A player with this id is going to join the lobby"

			//Future packet 15EA will contain character data (I think)
			//Then next comes a teleport position command to set the position of the
			//player.
			//Then it actually warps to the area with Sub60WarpToNewAreaCommand
			//Then it alerts everyone to its existence now in the zone with EnterFreshlyWrappedZoneCommand

			return Task.CompletedTask;
		}
	}
}
