using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	//Default non-generic is DefaultMovementGenerationStateState.
	public class DefaultMovementGeneratorState : DefaultMovementGeneratorState<DefaultMovementGenerationStateState>
	{
		/// <inheritdoc />
		public DefaultMovementGeneratorState(DefaultMovementGenerationStateState movementData) 
			: base(movementData)
		{

		}
	}

	public class DefaultMovementGeneratorState<TMovementGeneratorData> : BaseMovementGenerator<TMovementGeneratorData>
		where TMovementGeneratorData : class, IMovementStatePositionTargetable, IMovementStateSteppable
	{
		//This is based on what was in the old lerp component. No idea if this is the best approach, may have to re-examine.
		/// <inheritdoc />
		public override bool isFinished => MovementData.CurrentStep >= LerpDuration;

		/// <inheritdoc />
		public override bool isSkippable => true;

		//Old default for lerping was 0.2 seconds for regular remote PSOBB clients.
		public float LerpDuration = 0.2f;

		private Vector3 RealStartPosition { get; set; }

		protected Quaternion RealStartRotiation { get; private set; }

		private Quaternion RealEndRotation { get; set; }

		/// <inheritdoc />
		public DefaultMovementGeneratorState(TMovementGeneratorData movementData)
			: base(movementData)
		{

		}

		/// <inheritdoc />
		protected override void Start(GameObject entity, long currentTime)
		{
			//TODO: On start should we move the entity to the position specified as the StartPosition? They may not be at that point.
			RealStartPosition = entity.transform.position;
			RealStartRotiation = entity.transform.rotation;

			Vector3 dir = new Vector3(MovementData.TargetPosition.x, 0, MovementData.TargetPosition.y) - new Vector3(entity.transform.position.x, 0.0f, entity.transform.position.z);

			//TODO: This could be expensive, there might be a better way
			RealEndRotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
		}

		/// <inheritdoc />
		protected override void InternalUpdate(GameObject entity, long currentTime)
		{
			//TODO: We currently ignore currentTime. In the future we should base things off absolute time. We will
			//eventually transition PSO's networking to using timestamps for prediction and simulation syncronization for movement.
			float newX = Mathf.Lerp(RealStartPosition.x, MovementData.TargetPosition.x, MovementData.CurrentStep / LerpDuration);
			float newZ = Mathf.Lerp(RealStartPosition.z, MovementData.TargetPosition.y, MovementData.CurrentStep / LerpDuration);

			//Set the new lerped position and step the current step forward
			entity.transform.position = new Vector3(newX, entity.transform.position.y, newZ);
			ApplySlerpedRotation(entity);

			MovementData.CurrentStep += Time.deltaTime;

			//This is expected to only be called once, because callers should check isFinished.
			if(isFinished)
				OnFinished(entity);
		}

		protected virtual void ApplySlerpedRotation(GameObject entity)
		{
			entity.transform.rotation = Quaternion.Slerp(RealStartRotiation, RealEndRotation, MovementData.CurrentStep / LerpDuration);
		}

		protected virtual void OnFinished(GameObject entity)
		{

		}
	}
}
