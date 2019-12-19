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
	//GladMMO framework will try to send this when the client has moved or changed (or also as a movement heartbeat packet I think too)
	//You'd think I would know, since I wrote it, but I forget.
	//we don't use the base handler because we want to handle multiple types. We need to maintain state between multiple packets.
	[GladMMOHandler]
	public sealed class GladMMOInteropMovementInputHandler : IPeerMessageHandler<GameClientPacketPayload, PSOBBGamePacketPayloadClient> 
	{
		private IUnitScalerStrategy UnitScaler { get; }

		//I hate state like this but we need more information
		//for movement in PSOBB than in GladMMO.
		private ClientMovementDataUpdateRequest LastMovementRequest = null;

		private ClientRotationDataUpdateRequest LastRotationRequest = null;

		public GladMMOInteropMovementInputHandler(ILog logger, [NotNull] IUnitScalerStrategy unitScaler)
		{
			UnitScaler = unitScaler ?? throw new ArgumentNullException(nameof(unitScaler));
		}

		public async Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, ClientMovementDataUpdateRequest payload)
		{
			LastMovementRequest = payload;

			//This means movement stopped and PSO has a specific opcode for that.
			if (payload.MovementInput == Vector2.zero)
			{
				//TODO: Support rotation on movement stop.
				//Capture calling sync context just incase?
				await StopMovementAsync(context.PayloadSendService, payload.CurrentClientPosition, LastRotationRequest.Rotation);
			}
			else
			{
				//Capture calling sync context just incase?
				await UpdatedMovementLocation(context.PayloadSendService, payload.CurrentClientPosition);
			}
		}

		public async Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, ClientRotationDataUpdateRequest payload)
		{
			LastRotationRequest = payload;

			//This is not a feature of the PSOBB client, it's a NEW feature =).
			//Only broadcast rotation if the client isn't moving since PSOBB
			//just predicts the direction of a player based on movement otherwise
			if (LastMovementRequest != null && LastMovementRequest.MovementInput == Vector2.zero)
			{
				//TODO: Support rotation on movement stop.
				//Capture calling sync context just incase?
				await StopMovementAsync(context.PayloadSendService, LastMovementRequest.CurrentClientPosition, payload.Rotation);
			}
		}

		/// <inheritdoc />
		public Task StopMovementAsync(IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService, Vector3 position, float yAxisRotation)
		{
			//TODO: Support zone and room.
			return sendService.SendMessage(new Sub60FinishedMovingCommand(1,
				UnitScaler.ScaleYRotation(yAxisRotation),
				UnitScaler.UnScale(position).ToNetworkVector3(), 1, 15).ToPayload());
		}

		/// <inheritdoc />
		public Task UpdatedMovementLocation(IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService, Vector3 position)
		{
			return sendService.SendMessage(new Sub60MovingFastPositionSetCommand(1,
				UnitScaler.UnScaleYtoZ(position)).ToPayload());
		}

		public bool CanHandle(NetworkIncomingMessage<GameClientPacketPayload> message)
		{
			return message.Payload is ClientMovementDataUpdateRequest || message.Payload is ClientRotationDataUpdateRequest;
		}

		public async Task<bool> TryHandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, NetworkIncomingMessage<GameClientPacketPayload> message)
		{
			if (message.Payload is ClientMovementDataUpdateRequest c)
				await HandleMessage(context, c)
					.ConfigureAwait(false);
			else
			{
				//Must be the rotation packet then
				await HandleMessage(context, (ClientRotationDataUpdateRequest)message.Payload)
					.ConfigureAwait(false);
			}

			return true;
		}
	}
}
