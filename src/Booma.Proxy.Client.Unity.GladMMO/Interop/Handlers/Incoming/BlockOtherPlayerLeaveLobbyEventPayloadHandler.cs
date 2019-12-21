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

		/// <inheritdoc />
		public BlockOtherPlayerLeaveLobbyEventPayloadHandler(ILog logger, [NotNull] IInteropEntityMappable psoEntityKeyToGuidMappable)
			: base(logger)
		{
			PsoEntityKeyToGuidMappable = psoEntityKeyToGuidMappable ?? throw new ArgumentNullException(nameof(psoEntityKeyToGuidMappable));
		}

		public override async Task HandleMessage(InteropPSOBBPeerMessageContext context, BlockOtherPlayerLeaveLobbyEventPayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Player Slot: {payload.ClientId} left lobby.");

			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, (short)payload.ClientId);
			PsoEntityKeyToGuidMappable.RemoveEntityEntry(entityGuid);
		}
	}
}
