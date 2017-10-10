using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload sent to request a chunk of the guild card data.
	/// </summary>
	[WireDataContract]
	[LoginClientPacketPayload(LoginNetworkOperationCodes.BB_GUILDCARD_CHUNK_REQ_TYPE)]
	public sealed class LoginGuildCardChunkRequestPayload : PSOBBLoginPacketPayloadClient, IChunkRequest
	{
		//TODO: What is this?
		[WireMember(1)]
		private int unk { get; }

		/// <summary>
		/// The chunk number to request for
		/// the guild card data.
		/// </summary>
		[WireMember(2)]
		public uint ChunkNumber { get; }

		//TODO: What is this?
		/// <summary>
		/// ?
		/// </summary>
		[WireMember(3)]
		public bool ShouldContinue { get; }

		/// <inheritdoc />
		public LoginGuildCardChunkRequestPayload(uint chunkNumber, bool shouldContinue)
		{
			ChunkNumber = chunkNumber;
			ShouldContinue = shouldContinue;
		}

		private LoginGuildCardChunkRequestPayload()
		{
			
		}
	}
}
