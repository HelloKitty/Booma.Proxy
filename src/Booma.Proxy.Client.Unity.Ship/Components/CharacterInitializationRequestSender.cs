using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class CharacterInitializationRequestSender : MonoBehaviour
	{
		[Inject]
		public IClientPayloadSendService<PSOBBGamePacketPayloadClient> Client { get; }

		public void SendCharacterInitializationRequest()
		{
			//This request is stupid. Why is this a thing?
			Client.SendMessage(new BlockCharacterDataInitializeRequestPayload());
		}
	}
}
