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
	/// <summary>
	/// Handler that deals with the <see cref="Sub60MovingFastPositionSetCommand"/>
	/// event that is raised by the server when a client is moving fast/running.
	/// </summary>
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	public sealed class BlockMovingFastPositionChangedEventHandler : Command60Handler<Sub60MovingFastPositionSetCommand>
	{
		/// <summary>
		/// Service that translates the incoming position to the correct unit scale that
		/// Unity3D expects.
		/// </summary>
		private IUnitScalerStrategy Scaler { get; }

		private IEntityGuidMappable<WorldTransform> WorldTransformMappable { get; }

		private IEntityGuidMappable<MovementManager> MovementManagerMappable { get; }

		/// <inheritdoc />
		public BlockMovingFastPositionChangedEventHandler([NotNull] IUnitScalerStrategy scaler, ILog logger, IEntityGuidMappable<WorldTransform> worldTransformMappable, [NotNull] IEntityGuidMappable<MovementManager> movementManagerMappable)
			: base(logger)
		{
			Scaler = scaler ?? throw new ArgumentNullException(nameof(scaler));
			WorldTransformMappable = worldTransformMappable;
			MovementManagerMappable = movementManagerMappable ?? throw new ArgumentNullException(nameof(movementManagerMappable));
		}

		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60MovingFastPositionSetCommand command)
		{
			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier);

			//We can safely assume they have a known world transform or they can't have been spawned.
			//It's very possible, if this fails, that they are cheating/hacking or something.

			Vector2 position = Scaler.ScaleYasZ(command.Position);
			MovementManagerMappable[entityGuid].RegisterState(new DefaultMovementGeneratorState(new DefaultMovementGenerationStateState(position)));

			//New position commands should be direcly updating the entity's position. Even though "MovementGenerators" handle true movement by learping them.
			//They aren't the source of Truth since they aren't deterministic/authorative like is REAL MMOs. So, the true source of truth is the WorldTransform.
			Vector3 positionIn3dSpace = new Vector3(position.x, WorldTransformMappable[entityGuid].Position.y, position.y);
			WorldTransformMappable[entityGuid] = new WorldTransform(positionIn3dSpace, WorldTransformMappable[entityGuid].Rotation);

			return Task.CompletedTask;
		}
	}
}
