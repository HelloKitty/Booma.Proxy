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
	[Serializable]
	public sealed class EventDispatchNetworkTransform : INetworkEntityTransform
	{
		[Serializable]
		public class OnPositionChangedEvent : UnityEvent<Vector3> { }

		[Serializable]
		public class OnRotationChangedEvent : UnityEvent<Quaternion> { }

		[ShowInInspector]
		[ReadOnly]
		private Vector3 _position;

		[ShowInInspector]
		[ReadOnly]
		private Quaternion _rotation;

		[Tooltip("Dispatched event when the position is set.")]
		[SerializeField]
		private OnPositionChangedEvent OnPositionChanged;

		[Tooltip("Dispatcher event when the rotation is changed.")]
		[SerializeField]
		private OnRotationChangedEvent OnRotationChanged;

		/// <inheritdoc />
		public Vector3 Position
		{
			get => _position;
			set
			{
				_position = value;
				OnPositionChanged?.Invoke(_position);
			}
		}

		/// <inheritdoc />
		public Quaternion Rotation
		{
			get => _rotation;
			set
			{
				_rotation = value;
				OnRotationChanged?.Invoke(_rotation);
			}
		}

		/// <inheritdoc />
		public EventDispatchNetworkTransform(Vector3 position, Quaternion rotation)
		{
			Position = position;
			Rotation = rotation;
		}

		public EventDispatchNetworkTransform()
		{
			
		}
	}
}
