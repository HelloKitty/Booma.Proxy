using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class TargetPositionRotationMovementGeneratorInput : DefaultMovementGenerationStateState
	{
		/// <summary>
		/// The final/target rotation desired for movement.
		/// </summary>
		public Quaternion TargetRotation { get; }

		/// <inheritdoc />
		public TargetPositionRotationMovementGeneratorInput(Vector2 targetPosition, Quaternion targetRotation) 
			: base(targetPosition)
		{
			TargetRotation = targetRotation;
		}
	}
}
