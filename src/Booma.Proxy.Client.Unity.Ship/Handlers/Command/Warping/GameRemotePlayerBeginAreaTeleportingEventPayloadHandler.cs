using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using UnityEngine;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	public sealed class GameRemotePlayerBeginAreaTeleportingEventPayloadHandler : Command60Handler<Sub60ClientZoneTeleportingEventCommand>
	{
		private IEntityGuidMappable<MovementManager> MovementMappable { get; }

		private IEntityGuidMappable<GameObject> WorldObjectMappable { get; }

		/// <inheritdoc />
		public GameRemotePlayerBeginAreaTeleportingEventPayloadHandler([NotNull] ILog logger, [NotNull] IEntityGuidMappable<GameObject> worldObjectMappable, [NotNull] IEntityGuidMappable<MovementManager> movementMappable) 
			: base(logger)
		{
			WorldObjectMappable = worldObjectMappable ?? throw new ArgumentNullException(nameof(worldObjectMappable));
			MovementMappable = movementMappable ?? throw new ArgumentNullException(nameof(movementMappable));
		}

		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60ClientZoneTeleportingEventCommand command)
		{
			//This is one of the cases where we need to remove ONLY
			//the world representation of the remote player.
			//They're still in the game, so leave other data untouched,
			//but their world representation needs to be cleared.

			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier);

			MovementMappable.Remove(entityGuid);

			if(WorldObjectMappable.ContainsKey(entityGuid))
				GameObject.Destroy(WorldObjectMappable[entityGuid]);

			WorldObjectMappable.Remove(entityGuid);

			return Task.CompletedTask;
		}
	}
}
