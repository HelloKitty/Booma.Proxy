using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.Events;
using Unitysync.Async;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class GuildcardDataRequestSender : MonoBehaviour
	{
		[Inject]
		private IClientRequestSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		[Inject]
		private IClientPayloadSendService<PSOBBGamePacketPayloadClient> PayloadSendService { get; }

		[Inject]
		private ILog Logger { get; }

		[SerializeField]
		private UnityEvent OnFinishedReadingGuildCardData;

		public void RequestGuildCardData()
		{
			//Before we can read guild card data we must do some checksum shit
			Task.Factory.StartNew(DoChecksumProcess, TaskCreationOptions.LongRunning)
				.UnityAsyncContinueWith(this, () =>
				{
					//Read the guild card data async on another thread and when we're finished
					//we should invoke the onfinished event.
					Task.Factory
						.StartNew(ReadGuildCardDataFromServer, TaskCreationOptions.LongRunning)
						.UnityAsyncContinueWith(this, () => OnFinishedReadingGuildCardData?.Invoke())
						.ConfigureAwait(false);
				});
		}

		private async Task DoChecksumProcess()
		{
			//Syl doesn't check value, so I don't know what to send.
			LoginChecksumResponsePayload payload = await SendService.SendRequestAsync<LoginChecksumResponsePayload>(new LoginChecksumRequestPayload(0));
		}

		private async Task ReadGuildCardDataFromServer()
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug("Sending GuildData request.");

			LoginGuildCardDataHeaderResponsePayload payload = await SendService.SendRequestAsync<LoginGuildCardDataHeaderResponsePayload>(new LoginGuildRequestPayload());

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Guild Data Size: {payload.Length}.");

			//TODO: Validate length
			//At this point we have recieved a guildcard data header.
			//This header contains information needed to async read all the guild card data from the server
			byte[] guildcardDataBytes = new byte[payload.Length];

			//With the header we know how many bytes we should read
			for(uint chunkNumber = 0, byteReadCount = 0; byteReadCount < payload.Length; chunkNumber++)
			{
				//TODO: Should continue ever be false?
				LoginGuildCardChunkResponsePayload response = await SendService.SendRequestAsync<LoginGuildCardChunkResponsePayload>(new LoginGuildCardChunkRequestPayload(chunkNumber, true));

				if(Logger.IsDebugEnabled)
					Logger.Debug($"Recieved Chunk: {response.ChunkNumber} Size: {response.PartialData.Length}.");

				//Read the bytes into the created buffer
				Buffer.BlockCopy(response.PartialData, 0, guildcardDataBytes, (int)byteReadCount, response.PartialData.Length);
				byteReadCount += (uint)response.PartialData.Length;
			}

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Finished guild card data. Sending final ack.");

			//At this point we've read all bytes async from the server for guild card data.
			//However the client also sends a final chunk request with a cont of 0
			await PayloadSendService.SendMessage(new LoginGuildCardChunkRequestPayload(0, false));

			await Task.Delay(500); //enough time for server to see.
		}
	}
}
