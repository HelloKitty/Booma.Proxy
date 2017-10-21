using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class DefaultKickableBall : MonoBehaviour, IKickable
	{
		public float MoveSpeed = 10.0f;

		public float MoveDuration = 2.0f;

		/// <inheritdoc />
		public void Kick(Vector3 startPosition, float yAxisDirection)
		{
			//Force to start position
			transform.position = startPosition;
			transform.rotation = Quaternion.Euler(0, yAxisDirection, 0);
			StartCoroutine(MoveForward());
		}

		private IEnumerator MoveForward()
		{
			float time = 0.0f;

			while(time < MoveDuration)
			{
				transform.position += transform.forward * MoveSpeed * Time.deltaTime;
				time += Time.deltaTime;
				yield return null;
			}
		}
	}
}
