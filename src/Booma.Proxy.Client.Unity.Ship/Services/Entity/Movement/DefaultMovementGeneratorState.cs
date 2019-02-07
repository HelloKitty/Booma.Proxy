using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	//TODO: This is mostly just a test movement generator for now
	public sealed class DefaultMovementGeneratorState : BaseMovementGenerator<DefaultMovementGenerationStateState>
	{
		//This is based on what was in the old lerp component. No idea if this is the best approach, may have to re-examine.
		/// <inheritdoc />
		public override bool isFinished => MovementData.CurrentStep >= LerpDuration;

		/// <inheritdoc />
		public override bool isSkippable => true;

		//Old default for lerping was 0.2 seconds for regular remote PSOBB clients.
		public float LerpDuration = 0.2f;

		/// <inheritdoc />
		public DefaultMovementGeneratorState(DefaultMovementGenerationStateState movementData)
			: base(movementData)
		{

		}

		/// <inheritdoc />
		protected override void Start(GameObject entity, long currentTime)
		{
			//TODO: On start should we move the entity to the position specified as the StartPosition? They may not be at that point.
		}

		/// <inheritdoc />
		protected override void InternalUpdate(GameObject entity, long currentTime)
		{
			//TODO: We currently ignore currentTime. In the future we should base things off absolute time. We will
			//eventually transition PSO's networking to using timestamps for prediction and simulation syncronization for movement.
			float newX = Mathf.Lerp(MovementData.StartPosition.x, MovementData.TargetPosition.x, MovementData.CurrentStep / LerpDuration);
			float newZ = Mathf.Lerp(MovementData.StartPosition.z, MovementData.TargetPosition.y, MovementData.CurrentStep / LerpDuration);

			//Set the new lerped position and step the current step forward
			entity.transform.position = new Vector3(newX, entity.transform.position.y, newZ);

			MovementData.CurrentStep += Time.deltaTime;
		}
	}

	public sealed class DefaultMovementGenerationStateState
	{
		/// <summary>
		/// The current step of the lerp.
		/// Progress = CurrentStep / LerpDuration.
		/// </summary>
		public float CurrentStep { get; set; } = 0.0f;//public mutable.

		/// <summary>
		/// Cached target position to lerp towards.
		/// </summary>
		public Vector2 TargetPosition { get; }

		/// <summary>
		/// The cached start position to lerp towards.
		/// </summary>
		public Vector3 StartPosition { get; }

		/// <inheritdoc />
		public DefaultMovementGenerationStateState(Vector2 targetPosition, Vector3 startPosition)
		{
			TargetPosition = targetPosition;
			StartPosition = startPosition;
		}
	}
}
