using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Proxy
{
	public sealed class ShipSelectionScreenUIController : MonoBehaviour
	{
		[Serializable]
		public struct InterfaceStateTransition
		{
			public UnityEvent OnTransition;
		}

		[SerializeField]
		public List<InterfaceStateTransition> Transitions;

		public int InterfaceStateId = 0;

		public void StepInterfaceForward()
		{
			//If there is a specified transition we should invoke it.
			Transitions?[InterfaceStateId].OnTransition?.Invoke();

			InterfaceStateId++;
		}
	}
}
