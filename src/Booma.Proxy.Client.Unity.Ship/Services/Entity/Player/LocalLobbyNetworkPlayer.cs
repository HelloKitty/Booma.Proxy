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

		/// <inheritdoc />
		public override bool isLocalPlayer => true;

		void Start()
		{
			//Make camera follow us and set it's position to the point
			Camera.main.transform.parent = CameraPosition;
			Camera.main.transform.position = CameraPosition.position;
		}
	}
}
