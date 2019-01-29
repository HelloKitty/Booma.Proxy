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
	public sealed class BlockGameJoinEventPayloadHandler : GameMessageHandler<BlockGameJoinEventPayload>
	{
		private IUnitScalerStrategy UnitScaler { get; }

		private ICharacterSlotSelectedModel SlotModel { get; }

		/// <summary>
		/// Event broadcast right before we load the game scene.
		/// </summary>
		[Tooltip("Invoked before the game scene is loaded.")]
		[SerializeField]
		private UnityEvent OnBeforeGameJoin;

		//TODO: Implement proper scene/game loading
		public int TestGameSceneIndex = 0;

		/// <inheritdoc />
		public BlockGameJoinEventPayloadHandler([NotNull] ILog logger, [NotNull] IUnitScalerStrategy unitScaler, [NotNull] ICharacterSlotSelectedModel slotModel) 
			: base(logger)
		{
			UnitScaler = unitScaler ?? throw new ArgumentNullException(nameof(unitScaler));
			SlotModel = slotModel ?? throw new ArgumentNullException(nameof(slotModel));
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, BlockGameJoinEventPayload payload)
		{
			if(Logger.IsInfoEnabled)
			{
				Logger.Info($"Assigned Id: {payload.Identifier}");
				Logger.Info($"Players in room: {payload.Players.Count()}");
				foreach(var p in payload.Players)
					Logger.Info($"Player: {p}");

				Logger.Info($"{payload.Settings}");
			}

			//TODO: Remove this info logging.
			if(Logger.IsInfoEnabled)
			{
				StringBuilder builder = new StringBuilder();
				builder.Append("Variations: ");

				for(int i = 0; i < payload.Maps.Length; i++)
					builder.Append($"{i}: {payload.Maps[i]} ");

				Logger.Info(builder.ToString());
			}

			//We should init the slot model with our new identifer
			SlotModel.SlotSelected = payload.Identifier;

			//Then just like joining a lobby we should
			//broadcast that we're about to join a game
			//then load the scene responsible for handling the game we're entering.

			OnBeforeGameJoin?.Invoke();

			//TODO: Handle multiple different lobby scenes
			//Now we need to load the game/amp (probably pioneer 2 but I don't think it HAAS to be)
			SceneManager.LoadSceneAsync(TestGameSceneIndex).allowSceneActivation = true;

			//Don't send anything here, the server will send a 0x71 to let us know we should spawn


			//TODO: This is just test code to check if we can join a game
			/*await context.PayloadSendService.SendMessage(new Sub60TeleportToPositionCommand(payload.Identifier, UnitScaler.UnScale(TestSpawnPoint).ToNetworkVector3()).ToPayload());
			await context.PayloadSendService.SendMessage(new Sub60WarpToNewAreaCommand(payload.Identifier, 0).ToPayload()); //pioneer 2
			await context.PayloadSendService.SendMessage(new Sub60FinishedMapLoadCommand(payload.Identifier).ToPayload());

			await context.PayloadSendService.SendMessage(new BlockFinishedGameBurstingRequestPayload());*/

			//If there are others in the zone we should broadcast a 20
		}
	}
}
