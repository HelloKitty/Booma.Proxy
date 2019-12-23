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
	/// <summary>
	/// Base <see cref="BaseSubCommand60"/> handler for default movement generator
	/// movement command types.
	/// </summary>
	/// <typeparam name="TPositionChangeCommandType">The position changing event.</typeparam>
	public class BaseInteropDefaultPositionChangedEventHandler<TPositionChangeCommandType> : InteropCommand60Handler<TPositionChangeCommandType>
		where TPositionChangeCommandType : BaseSubCommand60, IMessageContextIdentifiable, IWorldPositionable<float>
	{
		/// <summary>
		/// Service that translates the incoming position to the correct unit scale that
		/// Unity3D expects.
		/// </summary>
		protected IUnitScalerStrategy Scaler { get; }

		private IEntityGuidMappable<int, GladMMO.WorldTransform> WorldTransformMappable { get; }

		private GladMMO.IEntityGuidMappable<GladMMO.WorldTransform> GladMMOWorldTransformMappable { get; }

		private IInteropEntityMappable GuidMappable { get; }

		/// <inheritdoc />
		public BaseInteropDefaultPositionChangedEventHandler([NotNull] IUnitScalerStrategy scaler, ILog logger, 
			IEntityGuidMappable<int, GladMMO.WorldTransform> worldTransformMappable,
			[NotNull] IInteropEntityMappable guidMappable,
			[NotNull] GladMMO.IEntityGuidMappable<GladMMO.WorldTransform> gladMmoWorldTransformMappable)
			: base(logger)
		{
			Scaler = scaler ?? throw new ArgumentNullException(nameof(scaler));
			WorldTransformMappable = worldTransformMappable;
			GuidMappable = guidMappable ?? throw new ArgumentNullException(nameof(guidMappable));
			GladMMOWorldTransformMappable = gladMmoWorldTransformMappable ?? throw new ArgumentNullException(nameof(gladMmoWorldTransformMappable));
		}

		protected override async Task HandleSubMessage(InteropPSOBBPeerMessageContext context, TPositionChangeCommandType command)
		{
			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier);

			if(!WorldTransformMappable.ContainsKey(entityGuid))
				throw new InvalidOperationException($"TODO: Handle error cases with no world transform while getting movement data.");

			NetworkEntityGuid networtkEntityGuid = GuidMappable[entityGuid];
			GladMMO.WorldTransform gladMMOworldTransform = GladMMOWorldTransformMappable[networtkEntityGuid];

			//We can safely assume they have a known world transform or they can't have been spawned.
			float currentYAxisPosition = GetYAxisPosition(networtkEntityGuid, command);
			Vector2 position2D = Scaler.ScaleYasZ(command.Position);
			Vector2 lastPosition2D = new Vector2(gladMMOworldTransform.PositionX, gladMMOworldTransform.PositionZ);

			//New position commands should be direcly updating the entity's position. Even though "MovementGenerators" handle true movement by learping them.
			//They aren't the source of Truth since they aren't deterministic/authorative like is REAL MMOs. So, the true source of truth is the WorldTransform.
			//Vector3 positionIn3dSpace = new Vector3(position2D.x, currentYAxisPosition, position2D.y);
			WorldTransformMappable[entityGuid] = new GladMMO.WorldTransform(position2D.x, currentYAxisPosition, position2D.y, WorldTransformMappable[entityGuid].YAxisRotation);

			Vector2 direction = ComputeMovementDirection(position2D, lastPosition2D);

			await context.GladMMOClientPayloadReceiver.SendMessage(new MovementDataUpdateEventPayload(new EntityAssociatedData<IMovementData>[1] {CreateMovementData(entityGuid, ComputeInitialMovementPosition(command, gladMMOworldTransform, currentYAxisPosition), direction) }));
		}

		protected virtual Vector3 ComputeInitialMovementPosition(TPositionChangeCommandType command, GladMMO.WorldTransform gladMMOworldTransform, float currentYAxisPosition)
		{
			return new Vector3(gladMMOworldTransform.PositionX, currentYAxisPosition, gladMMOworldTransform.PositionZ);
		}

		protected virtual float GetYAxisPosition(NetworkEntityGuid networtkEntityGuid, TPositionChangeCommandType command)
		{
			return GladMMOWorldTransformMappable[networtkEntityGuid].PositionY;
		}

		protected  virtual Vector2 ComputeMovementDirection(Vector2 position, Vector2 lastPosition)
		{
			return (position - lastPosition).normalized;
		}

		private EntityAssociatedData<IMovementData> CreateMovementData(int entityGuid, Vector3 initialPosition, Vector2 direction)
		{
			try
			{
				NetworkEntityGuid networkEntityGuid = GuidMappable[entityGuid];

				PositionChangeMovementData positionChangeData = new PositionChangeMovementData(DateTime.UtcNow.Ticks, initialPosition, direction, 0.0f);

				return new EntityAssociatedData<IMovementData>(networkEntityGuid, positionChangeData);
			}
			catch (Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to create Movement Data for: {entityGuid}. Reason: {e.ToString()}");

				throw;
			}
		}
	}
}
