using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladMMO;
using GladNet;
using Reinterpret.Net;

namespace Booma.Proxy
{
	[PSOBBHandler]
	public class InteropLoginJoinEventPayloadHandler : BasePSOBBIncomingInteropPayloadHandler<BlockLobbyJoinEventPayload>
	{
		private ICharacterSlotSelectedModel SlotModel { get; }

		private IInteropEntityMappable PsoEntityKeyToGuidMappable { get; }

		//This is required for the playerdetails guid to be accurate
		//GladMMO uses this as input.
		private ILocalCharacterDataRepository CharacterDataRepo { get; }

		/// <inheritdoc />
		public InteropLoginJoinEventPayloadHandler(ILog logger, [NotNull] ICharacterSlotSelectedModel slotModel,
			[NotNull] IInteropEntityMappable psoEntityKeyToGuidMappable,
			[NotNull] ILocalCharacterDataRepository characterDataRepo)
			: base(logger)
		{
			SlotModel = slotModel ?? throw new ArgumentNullException(nameof(slotModel));
			PsoEntityKeyToGuidMappable = psoEntityKeyToGuidMappable ?? throw new ArgumentNullException(nameof(psoEntityKeyToGuidMappable));
			CharacterDataRepo = characterDataRepo ?? throw new ArgumentNullException(nameof(characterDataRepo));
		}

		public override async Task HandleMessage(InteropPSOBBPeerMessageContext context, BlockLobbyJoinEventPayload payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"**Handling**: {nameof(BlockLobbyJoinEventPayload)}");

			//TODO: Init the slot we were assigned.
			//Just set the old char slot to the clientid
			//It's basically like a slot, like a lobby or party slot.
			SlotModel.SlotSelected = payload.ClientId;

			foreach (var entry in payload.LobbyCharacterData)
			{
				int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, (short)entry.PlayerHeader.ClientId);

				//TODO: Support guids larger than shorts.
				NetworkEntityGuid networkEntityGuid = NetworkEntityGuidBuilder.New()
					.WithType(GladMMO.EntityType.Player)
					.WithId((int)entry.PlayerHeader.GuildCardNumber)
					.Build();

				Logger.Debug($"RemotePlayer: {entry.PlayerHeader.CharacterName} GuildCard: {entry.PlayerHeader.GuildCardNumber} Slot: {entry.PlayerHeader.ClientId} PSOGuid: {entityGuid} GladMMOGUID: {networkEntityGuid.ToString()}");

				PsoEntityKeyToGuidMappable[entityGuid] = networkEntityGuid;

				if (SlotModel.SlotSelected == entry.PlayerHeader.ClientId)
					CharacterDataRepo.UpdateCharacterId((int)entry.PlayerHeader.GuildCardNumber);
			}

			//OnLocalPlayerLobbyJoined?.Invoke(this, new LobbyJoinedEventArgs(payload.LobbyNumber, EntityGuid.ComputeEntityGuid(EntityType.Player, payload.ClientId)));

			//Don't send anything here, the server will send a 0x60 0x6F after this
		}
	}
}
