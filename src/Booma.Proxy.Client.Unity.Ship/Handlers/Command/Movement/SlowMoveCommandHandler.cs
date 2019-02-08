using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class SlowMoveCommandHandler : BaseDefaultPositionChangedEventHandler<Sub60MovingSlowPositionSetCommand>
	{
		/// <inheritdoc />
		public SlowMoveCommandHandler(IUnitScalerStrategy scaler, ILog logger, IEntityGuidMappable<WorldTransform> worldTransformMappable, IEntityGuidMappable<MovementManager> movementManagerMappable) 
			: base(scaler, logger, worldTransformMappable, movementManagerMappable)
		{

		}
	}
}
