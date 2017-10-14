using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Handler that handles <see cref="LoginGuildCardDataHeaderResponsePayload"/>
	/// </summary>
	public sealed class LoginGuildCardDataHandler : LoginMessageHandler<LoginGuildCardDataHeaderResponsePayload>
	{
		/// <inheritdoc />
		public override async Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, LoginGuildCardDataHeaderResponsePayload payload)
		{
			//TODO: Validate length
			//At this point we have recieved a guildcard data header.
			//This header contains information needed to async read all the guild card data from the server
			byte[] guildcardDataBytes = new byte[payload.Length];

			//With the header we know how many bytes we should read
			for(uint chunkNumber = 0, byteReadCount = 0; byteReadCount < payload.Length; chunkNumber++)
			{
				//TODO: Should continue ever be false?
				LoginGuildCardChunkResponsePayload response = await context.RequestSendService.SendRequestAsync<LoginGuildCardChunkResponsePayload>(new LoginGuildCardChunkRequestPayload(chunkNumber, true));

				//Read the bytes into the created buffer
				Buffer.BlockCopy(response.PartialData, 0, guildcardDataBytes, (int)byteReadCount, response.PartialData.Length);
				byteReadCount += (uint)response.PartialData.Length;
			}

			//At this point we've read all bytes async from the server for guild card data.
			//However the client also sends a final chunk request with a cont of 0
			await context.PayloadSendService.SendMessage(new LoginGuildCardChunkRequestPayload(0, false));
		}
	}
}
