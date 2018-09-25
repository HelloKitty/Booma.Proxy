using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Booma.Proxy
{
	//based on: https://unity3d.college/2016/09/12/unity-oninspectorgui/
	[CustomEditor(typeof(SpawnPointComponent))]
	public class SpawnPointEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			StickSpawnerToGround();
			base.OnInspectorGUI();
		}

		private void StickSpawnerToGround()
		{
			SpawnPointComponent spawnPoint = target as SpawnPointComponent;

			if(spawnPoint == null)
				return;

			spawnPoint.transform.rotation = Quaternion.identity;

			RaycastHit hitInfo;
			if(Physics.Raycast(spawnPoint.transform.position, Vector3.down, out hitInfo, 10f)) //don't use a layer mask
			{
				spawnPoint.transform.position = new Vector3(spawnPoint.transform.position.x, hitInfo.point.y, spawnPoint.transform.position.z);
			}
		}
	}
}
