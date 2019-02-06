using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	[AdditionalRegisterationAs(typeof(ILocalPlayerLobbyJoinEventSubscribable))]
	[SceneTypeCreate(GameSceneType.PreBlockBurstingScene)]
	public class BlockLoginJoinEventPayloadHandler : GameMessageHandler<BlockLobbyJoinEventPayload>, ILocalPlayerLobbyJoinEventSubscribable
	{
		//TODO: Is it ok to reuse this?
		private ICharacterSlotSelectedModel SlotModel { get; }

		/// <inheritdoc />
		public event EventHandler<LobbyJoinedEventArgs> OnLocalPlayerLobbyJoined;

		/// <inheritdoc />
		public BlockLoginJoinEventPayloadHandler(ILog logger, [NotNull] ICharacterSlotSelectedModel slotModel) 
			: base(logger)
		{
			SlotModel = slotModel ?? throw new ArgumentNullException(nameof(slotModel));
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockLobbyJoinEventPayload payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"**Handling**: {nameof(BlockLobbyJoinEventPayload)}");

			//Just set the old char slot to the clientid
			//It's basically like a slot, like a lobby or party slot.
			SlotModel.SlotSelected = payload.ClientId;

			OnLocalPlayerLobbyJoined?.Invoke(this, new LobbyJoinedEventArgs(payload.LobbyNumber, EntityGuid.ComputeEntityGuid(EntityType.Player, payload.ClientId)));

			//Don't send anything here, the server will send a 0x60 0x6F after this
			return Task.CompletedTask;
		}
	}
}
