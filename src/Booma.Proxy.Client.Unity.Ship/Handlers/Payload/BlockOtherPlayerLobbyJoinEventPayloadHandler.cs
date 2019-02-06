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
	[AdditionalRegisterationAs(typeof(IRemotePlayerLobbyJoinEventSubscribable))]
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class BlockOtherPlayerLobbyJoinEventPayloadHandler : GameMessageHandler<BlockOtherPlayerJoinedLobbyEventPayload>, IRemotePlayerLobbyJoinEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler<LobbyJoinedEventArgs> OnRemotePlayerLobbyJoined;

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

			//Create the joined player
			//We don't really have a position for them though, it didn't come in this packet
			OnRemotePlayerLobbyJoined?.Invoke(this, new LobbyJoinedEventArgs(payload.LobbyNumber, EntityGuid.ComputeEntityGuid(EntityType.Player, payload.ClientId)));

			return Task.CompletedTask;
		}
	}
}
