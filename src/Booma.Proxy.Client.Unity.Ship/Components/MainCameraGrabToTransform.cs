using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Component will grab the Camera and parent it to
	/// the specified transform.
	/// </summary>
	public sealed class MainCameraGrabToTransform : MonoBehaviour
	{
		/// <summary>
		/// The Should be the transform the camera should be put in.
		/// </summary>
		[Required]
		[SerializeField]
		private Transform CameraPosition;

		void Start()
		{
			//Make camera follow the transform.
			Camera.main.transform.parent = CameraPosition;
			Camera.main.transform.position = CameraPosition.position;
		}
	}
}
