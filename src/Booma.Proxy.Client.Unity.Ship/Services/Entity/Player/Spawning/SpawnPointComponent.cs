using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class SpawnPointComponent : MonoBehaviour
	{
		//TODO: IFDEF this out for deployment builds
		private void OnDrawGizmos()
		{
			Color originalColor = Gizmos.color;

			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(transform.position, transform.localScale.magnitude);
			Gizmos.color = originalColor;

			//TODO: Add spawnpoint image icon
			Gizmos.DrawIcon(transform.position, "SpawnPoint.png", true);
		}
	}
}
