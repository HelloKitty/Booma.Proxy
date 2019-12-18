using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladMMO;
using GladNet;

namespace Booma.Proxy
{
	public class InteropLoginJoinEventPayloadHandler : BasePSOBBIncomingInteropPayloadHandler<BlockLobbyJoinEventPayload>
	{
		/// <inheritdoc />
		public BlockLoginJoinEventPayloadHandler(ILog logger)
			: base(logger)
		{

		}

		public override async Task HandleMessage(InteropPSOBBPeerMessageContext context, BlockLobbyJoinEventPayload payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"**Handling**: {nameof(BlockLobbyJoinEventPayload)}");

			//TODO: Init the slot we were assigned.
			//Just set the old char slot to the clientid
			//It's basically like a slot, like a lobby or party slot.
			//SlotModel.SlotSelected = payload.ClientId;

			//OnLocalPlayerLobbyJoined?.Invoke(this, new LobbyJoinedEventArgs(payload.LobbyNumber, EntityGuid.ComputeEntityGuid(EntityType.Player, payload.ClientId)));

			//Don't send anything here, the server will send a 0x60 0x6F after this
		}
	}
}
