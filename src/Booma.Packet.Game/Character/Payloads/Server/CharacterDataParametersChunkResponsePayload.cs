using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/* Blue Burst packet for sending a chunk of the parameter files.
	typedef struct bb_param_chunk
	{
		bb_pkt_hdr_t hdr;
		uint32_t chunk;
		uint8_t data[];
	}
	PACKED bb_param_chunk_pkt;*/

	//Syl: https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/packets.h#L1409
	/// <summary>
	/// Chunk response sent in response to <see cref="CharacterDataParametersChunkRequestPayload"/>.
	/// Contains partial byte chunk of the parameter data files.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BB_PARAM_CHUNK_TYPE)]
	public sealed class CharacterDataParametersChunkResponsePayload : PSOBBGamePacketPayloadServer, IChunkResponse
	{
		/// <summary>
		/// The id/number for this chunk.
		/// </summary>
		[WireMember(1)]
		public uint ChunkNumber { get; }

		/// <summary>
		/// Partial chunk for the parameter data.
		/// </summary>
		[ReadToEnd]
		[WireMember(2)]
		public byte[] PartialData { get; } = new byte[0]; //TODO: Idk why but for ReadToEnd we have to give it a default

		//Serializer ctor
		private CharacterDataParametersChunkResponsePayload()
		{
			
		}
	}
}
