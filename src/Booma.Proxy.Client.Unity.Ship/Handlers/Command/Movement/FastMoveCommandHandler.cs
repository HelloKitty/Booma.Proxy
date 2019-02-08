using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class FastMoveCommandHandler : BaseDefaultPositionChangedEventHandler<Sub60MovingFastPositionSetCommand>
	{
		/// <inheritdoc />
		public FastMoveCommandHandler(IUnitScalerStrategy scaler, ILog logger, IEntityGuidMappable<WorldTransform> worldTransformMappable, IEntityGuidMappable<MovementManager> movementManagerMappable) 
			: base(scaler, logger, worldTransformMappable, movementManagerMappable)
		{

		}
	}
}
