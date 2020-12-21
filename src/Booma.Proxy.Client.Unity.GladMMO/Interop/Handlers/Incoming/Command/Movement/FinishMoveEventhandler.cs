using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	[PSOBBHandler]
	public sealed class FinishMoveEventhandler : BaseInteropDefaultPositionChangedEventHandler<Sub60FinishedMovingCommand>
	{
		public FinishMoveEventhandler(IUnitScalerStrategy scaler,
			ILog logger,
			IEntityGuidMappable<int, GladMMO.WorldTransform> worldTransformMappable,
			IInteropEntityMappable guidMappable,
			GladMMO.IEntityGuidMappable<GladMMO.WorldTransform> gladMmoWorldTransformMappable)
			: base(scaler, logger, worldTransformMappable, guidMappable, gladMmoWorldTransformMappable)
		{

		}

		protected override Vector2 ComputeMovementDirection(Vector2 position, Vector2 lastPosition)
		{
			return Vector2.zero;
		}

		protected override float GetYAxisPosition(NetworkEntityGuid networtkEntityGuid, Sub60FinishedMovingCommand command)
		{
			return Scaler.ScaleY(command.Position.Y);
		}

		protected override Vector3 ComputeInitialMovementPosition(Sub60FinishedMovingCommand command, GladMMO.WorldTransform gladMMOworldTransform, float currentYAxisPosition)
		{
			return this.Scaler.Scale(command.Position);
		}
	}
}
