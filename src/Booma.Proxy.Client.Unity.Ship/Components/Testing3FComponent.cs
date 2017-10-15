using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public class Testing3FComponent : SerializedMonoBehaviour
	{
		[Inject]
		private IClientPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		[Inject]
		private ICharacterSlotSelectedModel SlotModel { get; }

		[SerializeField]
		public Vector3 Position;

		[Button("Send 3F")]
		public void Test3F()
		{
			SendService.SendMessage(new Sub60TeleportToPositionCommand((byte)SlotModel.SlotSelected, new Vector3<float>(Position.x, Position.y, Position.z)).ToPayload());
		}
	}
}
