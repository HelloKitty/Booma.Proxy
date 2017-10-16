using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class TransformLerpTowards : MonoBehaviour
	{
		[HideInPlayMode]
		[MinValue(0)]
		[SerializeField]
		public float LerpDuration;

		/// <summary>
		/// The current step of the lerp.
		/// Progress = CurrentStep / LerpDuration.
		/// </summary>
		private float CurrentStep;

		/// <summary>
		/// Cached target position to lerp towards.
		/// </summary>
		private Vector2 TargetPosition;

		/// <summary>
		/// The cached start position to lerp towards.
		/// </summary>
		private Vector3 StartPosition;

		private void Start()
		{
			CurrentStep = float.MaxValue;
			StartCoroutine(LerpTowards());
		}

		public void OnRecievedNewPosition(Vector2 positionXZ)
		{
			//Init new lerp data
			StartPosition = transform.position;
			TargetPosition = positionXZ;

			//Reset the lerp duration
			CurrentStep = 0;
		}

		private IEnumerator LerpTowards()
		{
			while(true)
			{
				if(CurrentStep <= LerpDuration)
				{
					float newX = Mathf.Lerp(StartPosition.x, TargetPosition.x, CurrentStep / LerpDuration);
					float newZ = Mathf.Lerp(StartPosition.z, TargetPosition.y, CurrentStep / LerpDuration);

					//Set the new lerped position and step the current step forward
					transform.position = new Vector3(newX, transform.position.y, newZ);
					CurrentStep += Time.deltaTime;
				}

				yield return null;
			}
		}
	}
}
