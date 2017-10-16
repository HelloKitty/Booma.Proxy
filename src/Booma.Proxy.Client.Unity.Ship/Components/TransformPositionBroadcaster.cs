using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Component that can broadcast the transform of the object.
	/// </summary>
	[Injectee]
	public sealed class TransformPositionBroadcaster : MonoBehaviour
	{
		[MaxValue(200)]
		[MinValue(1)]
		[SerializeField]
		public int BroadcastsPerSecond;

		[Inject]
		private ICharacterSlotSelectedModel SlotModel { get; }

		[Inject]
		private IClientPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		public Vector3 TempScale = new Vector3(0.2f, 0.2f, -0.2f);

		//Cached last position
		private Vector3 lastPosition;

		private bool isFinishedMoving = true;

		public void StartBroadcasting()
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
					SendService.SendMessage(new Sub60MovingFastPositionSetCommand((byte)SlotModel.SlotSelected, new Vector2<float>(transform.position.x * TempScale.x, transform.position.z * TempScale.z)).ToPayload());
				}
				else if(!isFinishedMoving)
				{
					lastPosition = transform.position;
					isFinishedMoving = true;
					Vector3 pos = Vector3.Scale(transform.position, TempScale);
	
					//TODO: Handle rotation
					//Send a stop if we stopped moving
					SendService.SendMessage(new Sub60FinishedMovingCommand((byte)SlotModel.SlotSelected, (ushort)(transform.rotation.eulerAngles.y * 180f), new Vector3<float>(pos.x, pos.y, pos.z)).ToPayload());
				}

				yield return new WaitForSeconds(1.0f / BroadcastsPerSecond);
			}
		}
	}
}
