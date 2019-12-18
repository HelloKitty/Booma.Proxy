using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladMMO;
using GladNet;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// The handler for <see cref="Sub60ClientBurstBeginEventCommand"/> which handles this payload
	/// alerting 
	/// </summary>
	[PSOBBHandler]
	public sealed class InteropSub60ClientBurstBeginEventCommandHandler : InteropCommand60Handler<Sub60ClientBurstBeginEventCommand>
	{
		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		/// <inheritdoc />
		public InteropSub60ClientBurstBeginEventCommandHandler(ILog logger,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails)
			: base(logger)
		{
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		/// <inheritdoc />
		protected override async Task HandleSubMessage(InteropPSOBBPeerMessageContext context, Sub60ClientBurstBeginEventCommand payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"PSOBB: {this.MessageName()}.");

			//This means we're bursting into the game, aka spawning I guess.
			//So send the self-spawn event to the local client
			await context.GladMMOClientPayloadReceiver.SendMessage(new PlayerSelfSpawnEventPayload(new EntityCreationData(PlayerDetails.LocalPlayerGuid, CreateInitialMovementData(), CreateInitialFieldValues())))
				.ConfigureAwait(false);
		}

		private FieldValueUpdate CreateInitialFieldValues()
		{
			//TODO: Initialize player data somehow.
			return new FieldValueUpdate(new WireReadyBitArray(GladMMOCommonConstants.PLAYER_DATA_FIELD_SIZE * 32, false), new int[0]);
		}

		private static PositionChangeMovementData CreateInitialMovementData()
		{
			//TODO: Get spawnpoint some other way, this is a hack for the lobby demo.
			return new PositionChangeMovementData(DateTime.UtcNow.Ticks, new Vector3(-0.73f, 4.001f, -30.22f), Vector2.zero, 0.0f);
		}
	}
}
