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

		[SerializeField]
		private OnPositionChangedEvent OnPositionChanged;

		public void Update()
		{
			float hor = Input.GetAxis("Vertical");
			float ver = Input.GetAxis("Horizontal");
			transform.position = new Vector3(hor * Speed * Time.deltaTime + transform.position.x, transform.position.y, ver * Speed * Time.deltaTime + transform.position.z);

			if(Input.GetKeyDown(KeyCode.E))
				transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.y + Speed * Time.deltaTime, Vector3.up);
			else if(Input.GetKeyDown(KeyCode.Q))
				transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.y + -Speed * Time.deltaTime, Vector3.up);

			if(Math.Abs(hor) > float.Epsilon || Math.Abs(ver) > float.Epsilon || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
				OnPositionChanged?.Invoke(transform.position);
		}
	}
}
