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
	//TODO: Rewrite
	[PSOBBHandler]
	public sealed class BlockOtherPlayerLeaveLobbyEventPayloadHandler : BasePSOBBIncomingInteropPayloadHandler<BlockOtherPlayerLeaveLobbyEventPayload>
	{
		private IInteropEntityMappable PsoEntityKeyToGuidMappable { get; }

		private IReadOnlyCollection<IEntityCollectionRemovable<int>> ComponentRemovables { get; }

		/// <inheritdoc />
		public BlockOtherPlayerLeaveLobbyEventPayloadHandler(ILog logger, [NotNull] IInteropEntityMappable psoEntityKeyToGuidMappable, [NotNull] IReadOnlyCollection<IEntityCollectionRemovable<int>> componentRemovables)
			: base(logger)
		{
			PsoEntityKeyToGuidMappable = psoEntityKeyToGuidMappable ?? throw new ArgumentNullException(nameof(psoEntityKeyToGuidMappable));
			ComponentRemovables = componentRemovables ?? throw new ArgumentNullException(nameof(componentRemovables));
		}

		public override async Task HandleMessage(InteropPSOBBPeerMessageContext context, BlockOtherPlayerLeaveLobbyEventPayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Player Slot: {payload.ClientId} left lobby.");

			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, (short)payload.ClientId);

			//This tells GladMMO that the player is despawning.
			await context.GladMMOClientPayloadReceiver.SendMessage(new NetworkObjectVisibilityChangeEventPayload(Array.Empty<EntityCreationData>(), new NetworkEntityGuid[1] {PsoEntityKeyToGuidMappable[entityGuid]}));
			
			PsoEntityKeyToGuidMappable.RemoveEntityEntry(entityGuid);

			foreach (var removable in ComponentRemovables)
				removable.RemoveEntityEntry(entityGuid);
		}
	}
}
