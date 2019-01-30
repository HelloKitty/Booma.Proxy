using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	//Based on the old: GuildcardDataRequestSender. (deleted)
	//TODO: We don't even do anything with this data. We should.
	[NetworkMessageHandler(GameSceneType.CharacterSelectionScreen)]
	public sealed class CharacterGuildCardDataHeaderResponseHandler : GameMessageHandler<CharacterGuildCardDataHeaderResponsePayload>
	{
		/// <inheritdoc />
		public CharacterGuildCardDataHeaderResponseHandler(ILog logger) 
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, CharacterGuildCardDataHeaderResponsePayload payload)
		{
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
				CharacterGuildCardChunkResponsePayload response = await context.RequestSendService.SendRequestAsync<CharacterGuildCardChunkResponsePayload>(new CharacterGuildCardChunkRequestPayload(chunkNumber, true));

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
			await context.PayloadSendService.SendMessage(new CharacterGuildCardChunkRequestPayload(0, false));

			await Task.Delay(500); //enough time for server to see.

			//The old editor/scene handlers eventually ended up
			//loading the next scene after all that
			SceneManager.LoadSceneAsync(2).allowSceneActivation = true;
		}
	}
}
