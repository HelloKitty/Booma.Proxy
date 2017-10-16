using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public class SimpleMovementTestController : MonoBehaviour
	{
		public float Speed;

		public void Update()
		{
			transform.position = new Vector3(Input.GetAxis("Horizontal") * Speed * Time.deltaTime + transform.position.x, transform.position.y, Input.GetAxis("Vertical") * Speed * Time.deltaTime + transform.position.z);
		}
	}
}
