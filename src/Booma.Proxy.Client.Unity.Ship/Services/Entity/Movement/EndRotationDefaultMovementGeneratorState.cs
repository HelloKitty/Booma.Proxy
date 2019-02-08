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
		/// <inheritdoc />
		public EndRotationDefaultMovementGeneratorState(TargetPositionRotationMovementGeneratorInput movementData) 
			: base(movementData)
		{

		}

		/// <inheritdoc />
		protected override void OnFinished([NotNull] GameObject entity)
		{
			if(entity == null) throw new ArgumentNullException(nameof(entity));

			//The movement is finished, we should set rotation here.
			entity.transform.rotation = Quaternion.AngleAxis(MovementData.TargetYAxisRotation, Vector3.up);
		}
	}
}
