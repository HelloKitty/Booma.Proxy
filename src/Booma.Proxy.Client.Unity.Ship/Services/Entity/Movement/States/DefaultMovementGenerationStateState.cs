using UnityEngine;

namespace Booma.Proxy
{
	public class DefaultMovementGenerationStateState : IMovementStateSteppable, IMovementStatePositionTargetable
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

		/// <inheritdoc />
		public DefaultMovementGenerationStateState(Vector2 targetPosition)
		{
			TargetPosition = targetPosition;
		}
	}
}