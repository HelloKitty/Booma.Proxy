using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Proxy
{
	public sealed class DebugEventDispatcher : SerializedMonoBehaviour
	{
		[SerializeField]
		private UnityEvent OnDebugEvent;

		[Button("Debug Event Dispatch")]
		public void DispatchEvent()
		{
			OnDebugEvent?.Invoke();
		}
	}
}
