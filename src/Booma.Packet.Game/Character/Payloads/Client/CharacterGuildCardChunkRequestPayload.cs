using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Payload sent to request a chunk of the guild card data.
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.BB_GUILDCARD_CHUNK_REQ_TYPE)]
	public sealed partial class CharacterGuildCardChunkRequestPayload : PSOBBGamePacketPayloadClient, IChunkRequest
	{
		//Tethella does this check on the unk for some reason.
		//if ((client->decryptbuf[0x08] == 0x01) && (client->decryptbuf[0x10] == 0x01))

		//TODO: What is this?
		[WireMember(1)]
		internal int unk { get; set; } = 1; //this is 0x08

		/// <summary>
		/// The chunk number to request for
		/// the guild card data.
		/// </summary>
		[WireMember(2)]
		public uint ChunkNumber { get; internal set; }

		//TODO: What is this?
		/// <summary>
		/// ?
		/// </summary>
		[WireMember(3)]
		public bool ShouldContinue { get; internal set; } //this is 0x10

		/// <inheritdoc />
		public CharacterGuildCardChunkRequestPayload(uint chunkNumber, bool shouldContinue)
			: this()
		{
			ChunkNumber = chunkNumber;
			ShouldContinue = shouldContinue;
		}

		public CharacterGuildCardChunkRequestPayload()
			: base(GameNetworkOperationCode.BB_GUILDCARD_CHUNK_REQ_TYPE)
		{
			
		}
	}
}
