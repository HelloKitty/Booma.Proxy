using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class LocalLobbyNetworkPlayer : LobbyNetworkPlayer
	{
		/// <summary>
		/// The Should be the transform the camera should be put in.
		/// </summary>
		[Required]
		[SerializeField]
		private Transform CameraPosition;

		void Start()
		{
			Camera.main.transform.parent = CameraPosition;
		}
	}
}
