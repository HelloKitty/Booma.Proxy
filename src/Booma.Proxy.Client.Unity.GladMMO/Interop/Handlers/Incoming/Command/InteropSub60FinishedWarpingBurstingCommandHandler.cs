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
	//This command is sent by other players when we're freshly warping in so that we
	//know where they are after we spawn/warp.
	/// <summary>
	/// The handler for <see cref="Sub60ClientBurstBeginEventCommand"/> which handles this payload
	/// alerting 
	/// </summary>
	[PSOBBHandler]
	public sealed class InteropSub60FinishedWarpingBurstingCommandHandler : InteropCommand60Handler<Sub60FinishedWarpingBurstingCommand>
	{
		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		private IUnitScalerStrategy UnitScaler { get; }

		private GladMMO.IReadonlyEntityGuidMappable<GladMMO.WorldTransform> WorldTransformMappable { get; } 

		/// <inheritdoc />
		public InteropSub60FinishedWarpingBurstingCommandHandler(ILog logger,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] IUnitScalerStrategy unitScaler,
			[NotNull] GladMMO.IReadonlyEntityGuidMappable<GladMMO.WorldTransform> worldTransformMappable)
			: base(logger)
		{
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			UnitScaler = unitScaler ?? throw new ArgumentNullException(nameof(unitScaler));
			WorldTransformMappable = worldTransformMappable ?? throw new ArgumentNullException(nameof(worldTransformMappable));
		}

		protected override async Task HandleSubMessage(InteropPSOBBPeerMessageContext context, Sub60FinishedWarpingBurstingCommand command)
		{
			GladMMO.WorldTransform transform = WorldTransformMappable.RetrieveEntity(PlayerDetails.LocalPlayerGuid);
			Vector3 position = new Vector3(transform.PositionX, transform.PositionY, transform.PositionZ);

			Vector3<float> scaledPosition = UnitScaler.UnScale(position).ToNetworkVector3();
			float scaledRotation = UnitScaler.ScaleYRotation(transform.YAxisRotation);

			await context.PayloadSendService.SendMessage(new Sub60FinishedWarpAckCommand(1, 15, scaledPosition, scaledRotation).ToPayload());
		}
	}
}
