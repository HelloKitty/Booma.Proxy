using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;
using UnityEngine;

namespace Booma.Proxy
{
	[PSOBBHandler]
	public sealed class InteropSub60TeleportToPositionCommandHandler : InteropCommand60Handler<Sub60TeleportToPositionCommand>
	{
		private IEntityGuidMappable<int, GladMMO.WorldTransform> WorldTransformMappable { get; }

		private IUnitScalerStrategy UnitScaler { get; }

		public InteropSub60TeleportToPositionCommandHandler(ILog logger,
			[NotNull] IEntityGuidMappable<int, GladMMO.WorldTransform> worldTransformMappable,
			[NotNull] IUnitScalerStrategy unitScaler) 
			: base(logger)
		{
			WorldTransformMappable = worldTransformMappable ?? throw new ArgumentNullException(nameof(worldTransformMappable));
			UnitScaler = unitScaler ?? throw new ArgumentNullException(nameof(unitScaler));
		}

		protected override async Task HandleSubMessage(InteropPSOBBPeerMessageContext context, Sub60TeleportToPositionCommand command)
		{
			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier);
			Vector3 position = UnitScaler.Scale(command.Position);

			//TODO: Set position in GladMMO
			WorldTransformMappable[entityGuid] = new GladMMO.WorldTransform(position.x, position.y, position.z, UnitScaler.ScaleYRotation(command.Identifier));
		}
	}
}
