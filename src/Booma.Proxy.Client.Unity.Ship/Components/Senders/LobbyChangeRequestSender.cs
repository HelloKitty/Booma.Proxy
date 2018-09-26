using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class LobbyChangeRequestSender : MonoBehaviour
	{
		[Inject]
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		/// <summary>
		/// Sends a new lobby change request.
		/// </summary>
		/// <param name="newLobbyNumber">The lobby to change to.</param>
		public void SendChangeRequest(int newLobbyNumber)
		{
			//Just send a 0 id menu with the new lobby number as the item id.
			SendService.SendMessage(new BlockLobbyChangeRequestPayload(new MenuItemIdentifier(0, (byte)newLobbyNumber)));
		}
	}
}
