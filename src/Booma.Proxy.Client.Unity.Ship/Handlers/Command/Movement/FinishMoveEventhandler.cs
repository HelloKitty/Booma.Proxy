using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class FinishMoveEventhandler : BaseDefaultPositionChangedEventHandler<Sub60FinishedMovingCommand>
	{
		/// <inheritdoc />
		public FinishMoveEventhandler(IUnitScalerStrategy scaler, ILog logger, IEntityGuidMappable<WorldTransform> worldTransformMappable, IEntityGuidMappable<MovementManager> movementManagerMappable) 
			: base(scaler, logger, worldTransformMappable, movementManagerMappable)
		{

		}

		/// <inheritdoc />
		protected override IMovementGeneratorState CreateMovementGenerator(Vector2 position, [NotNull] Sub60FinishedMovingCommand command)
		{
			if(command == null) throw new ArgumentNullException(nameof(command));

			return new EndRotationDefaultMovementGeneratorState(new TargetPositionRotationMovementGeneratorInput(position, Quaternion.AngleAxis(Scaler.ScaleYRotation(command.YAxisRotation), Vector3.up)));
		}
	}
}
