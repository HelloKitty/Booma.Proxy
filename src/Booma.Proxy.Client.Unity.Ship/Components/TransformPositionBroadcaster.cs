using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Component that can broadcast the transform of the object.
	/// </summary>
	[Injectee]
	public sealed class TransformPositionBroadcaster : SerializedMonoBehaviour
	{
		[MaxValue(200)]
		[MinValue(1)]
		[SerializeField]
		public int BroadcastsPerSecond;

		/// <summary>
		/// The injected identity for the broadcasted transform.
		/// </summary>
		[Inject]
		private IEntityIdentity Identity { get; }

		[Inject]
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		[Inject]
		private IUnitScalerStrategy UnitScaler { get; }

		[Inject]
		private IZoneSettings ZoneData { get; }

		[Inject]
		private IRoomQueryable RoomQueryService { get; }

		//Cached last position
		private Vector3 lastPosition;

		private bool isFinishedMoving = true;

		public void Start()
		{
			lastPosition = transform.position;
			StartCoroutine(BroadcastTransformPosition());
		}

		private IEnumerator BroadcastTransformPosition()
		{
			while(true)
			{
				if(Vector3.Magnitude(lastPosition - transform.position) > Vector3.kEpsilon)
				{
					lastPosition = transform.position;
					isFinishedMoving = false;
					SendService.SendMessage(new Sub60MovingFastPositionSetCommand(Identity.EntityId, 
						UnitScaler.UnScaleYtoZ(transform.position)).ToPayload());
				}
				else if(!isFinishedMoving)
				{
					lastPosition = transform.position;
					isFinishedMoving = true;

					//TODO: Handle rotation
					//Send a stop if we stopped moving
					SendService.SendMessage(new Sub60FinishedMovingCommand(Identity.EntityId, 
						UnitScaler.ScaleYRotation(transform.rotation.eulerAngles.y), 
						UnitScaler.UnScale(transform.position).ToNetworkVector3(), RoomQueryService.RoomIdForPlayerById(Identity.EntityId), ZoneData.ZoneId).ToPayload());
				}

				yield return new WaitForSeconds(1.0f / BroadcastsPerSecond);
			}
		}
	}
}
