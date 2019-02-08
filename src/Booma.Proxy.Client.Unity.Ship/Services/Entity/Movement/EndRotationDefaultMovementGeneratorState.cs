using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class EndRotationDefaultMovementGeneratorState : DefaultMovementGeneratorState<TargetPositionRotationMovementGeneratorInput>
	{
		//Don't skip ending movement.
		/// <inheritdoc />
		public override bool isSkippable => false;

		/// <inheritdoc />
		public EndRotationDefaultMovementGeneratorState(TargetPositionRotationMovementGeneratorInput movementData) 
			: base(movementData)
		{

		}

		/// <inheritdoc />
		protected override void ApplySlerpedRotation(GameObject entity)
		{
			//We can do better here since we known the true rotation
			entity.transform.rotation = Quaternion.Slerp(RealStartRotiation, MovementData.TargetRotation, MovementData.CurrentStep / LerpDuration);
		}

		/// <inheritdoc />
		protected override void OnFinished([NotNull] GameObject entity)
		{
			if(entity == null) throw new ArgumentNullException(nameof(entity));

			//The movement is finished, we should set rotation here.
			entity.transform.rotation = MovementData.TargetRotation;
		}
	}
}
