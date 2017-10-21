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
			float ver = Input.GetAxis("Vertical");
			float hor = Input.GetAxis("Horizontal");

			transform.position += transform.forward * ver * Speed * Time.deltaTime + transform.right * hor * Speed * Time.deltaTime;

			if(Input.GetKey(KeyCode.E))
				transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.y + Speed * Time.deltaTime * 10.0f, Vector3.up);
			else if(Input.GetKey(KeyCode.Q))
				transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.y + -Speed * Time.deltaTime * 10.0f, Vector3.up);

			if(Math.Abs(hor) > float.Epsilon || Math.Abs(ver) > float.Epsilon || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
				OnPositionChanged?.Invoke(transform.position);
		}
	}
}
