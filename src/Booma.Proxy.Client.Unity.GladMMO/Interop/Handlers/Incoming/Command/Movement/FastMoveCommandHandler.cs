using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;

namespace Booma.Proxy
{
	[PSOBBHandler]
	public sealed class FastMoveCommandHandler : BaseInteropDefaultPositionChangedEventHandler<Sub60MovingFastPositionSetCommand>
	{
		/// <inheritdoc />
		public FastMoveCommandHandler(IUnitScalerStrategy scaler,
			ILog logger,
			IEntityGuidMappable<int, GladMMO.WorldTransform> worldTransformMappable,
			IInteropEntityMappable guidMappable,
			GladMMO.IEntityGuidMappable<GladMMO.WorldTransform> gladMmoWorldTransformMappable)
			: base(scaler, logger, worldTransformMappable, guidMappable, gladMmoWorldTransformMappable)
		{

		}
	}
}
