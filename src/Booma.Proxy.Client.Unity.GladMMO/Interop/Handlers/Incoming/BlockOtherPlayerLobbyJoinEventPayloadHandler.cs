using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;

namespace Booma.Proxy
{
	//TODO: Consolidate with 0x67 packet handling
	//Another player joined the lobby/game BUT we don't have enough information to spawn them
	//here so we just preparing their NetworkEntityGuid mapping by slot.
	[PSOBBHandler]
	public sealed class BlockOtherPlayerLobbyJoinEventPayloadHandler : BasePSOBBIncomingInteropPayloadHandler<BlockOtherPlayerJoinedLobbyEventPayload>
	{
		private IInteropEntityMappable PsoEntityKeyToGuidMappable { get; }

		/// <inheritdoc />
		public BlockOtherPlayerLobbyJoinEventPayloadHandler(ILog logger, [NotNull] IInteropEntityMappable psoEntityKeyToGuidMappable)
			: base(logger)
		{
			PsoEntityKeyToGuidMappable = psoEntityKeyToGuidMappable ?? throw new ArgumentNullException(nameof(psoEntityKeyToGuidMappable));
		}


		public override async Task HandleMessage(InteropPSOBBPeerMessageContext context, BlockOtherPlayerJoinedLobbyEventPayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Player Lobby Join: {payload.ClientId} LeaderId: {payload.LeaderId} EventId: {payload.EventId} Lobby: {payload.LobbyNumber} Name: {payload.JoinData.PlayerHeader.CharacterName} GuildCard: {payload.JoinData.PlayerHeader.GuildCardNumber}");

			//Legacy comment
			//Future packet 15EA will contain character data (I think)
			//Then next comes a teleport position command to set the position of the
			//player.
			//Then it actually warps to the area with Sub60WarpToNewAreaCommand
			//Then it alerts everyone to its existence now in the zone with EnterFreshlyWrappedZoneCommand
			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, (short)payload.JoinData.PlayerHeader.ClientId);

			//TODO: Support guids larger than shorts.
			NetworkEntityGuid networkEntityGuid = NetworkEntityGuidBuilder.New()
				.WithType(GladMMO.EntityType.Player)
				.WithId((int)payload.JoinData.PlayerHeader.GuildCardNumber)
				.Build();

			PsoEntityKeyToGuidMappable[entityGuid] = networkEntityGuid;

			//At the very least, this is how we should handle a player joing the lobby.
			//Games might be different.
		}
	}
}
