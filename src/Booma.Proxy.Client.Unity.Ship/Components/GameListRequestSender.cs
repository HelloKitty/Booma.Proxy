using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using UnityEngine.UI;
using Unitysync.Async;

namespace Booma.Proxy
{
	/// <summary>
	/// Sends the game list request to the server.
	/// </summary>
	public sealed class GameListRequestSender : NetworkRequestSender
	{
		[Button("Send Gamelist Request")]
		public void SendGameListRequest()
		{
			//Sends the request and gets the game list response in the continuation method
			SendServiceAsync.SendRequestAsync<BlockGameListResponsePayload>(new BlockGameListRequestPayload())
				.UnityAsyncContinueWith(this, HandleGameListResponse);
		}

		private void HandleGameListResponse(BlockGameListResponsePayload response)
		{
			//TODO: This is just for testing, we try to join the first one list
			GameListEntry entry = response.GameEntries.First();

			SendService.SendMessage(new SharedMenuSelectionRequestPayload(entry.Listing), DeliveryMethod.ReliableOrdered);
		}
	}
}
