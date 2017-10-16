using System;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Proxy
{
	[Serializable]
	public class OnPositionChangedEvent : UnityEvent<Vector2> { }
}