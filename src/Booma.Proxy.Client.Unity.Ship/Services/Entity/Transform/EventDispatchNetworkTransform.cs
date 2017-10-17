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
	public class OnPositionChangedEvent : UnityEvent<Vector3> { }

	[Serializable]
	public class OnRotationChangedEvent : UnityEvent<Quaternion> { }

	[Serializable]
	public sealed class EventDispatchNetworkTransform : MonoBehaviour, INetworkEntityTransform
	{
		[ShowInInspector]
		[ReadOnly]
		private Vector3 _position;

		[ShowInInspector]
		[ReadOnly]
		private Quaternion _rotation;

		[Tooltip("Indicates if the transform should set its initial position and rotation to the GameObject's on Awake.")]
		[SerializeField]
		public bool ShouldInitializeOnAwake;

		[SerializeField]
		[Tooltip("Dispatched event when the position is set.")]
		private OnPositionChangedEvent OnPositionChanged;

		[SerializeField]
		[Tooltip("Dispatcher event when the rotation is changed.")]
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

		void Awake()
		{
			if(ShouldInitializeOnAwake)
			{
				_position = transform.position;
				_rotation = transform.rotation;
			}
		}

		/// <summary>
		/// Sets the position without broadcasting.
		/// </summary>
		/// <param name="postion">The positon to set.</param>
		public void SetPositionNoBroadcast(Vector3 postion)
		{
			//Don't use the property to avoid broadcast
			_position = postion;
		}
	}
}
