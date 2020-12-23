using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BB_GUILDCARD_CHUNK_TYPE)]
	public sealed class CharacterGuildCardChunkResponsePayload : PSOBBGamePacketPayloadServer, IChunkResponse
	{
		//TODO: What is this?
		[WireMember(1)]
		internal int unk { get; set; }

		/// <summary>
		/// The chunk number of this partial data.
		/// </summary>
		[WireMember(2)]
		public uint ChunkNumber { get; internal set; }

		//The maximum size
		/// <summary>
		/// This is a partial data chunk of the guild card
		/// data.
		/// See: https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/player.h#L272
		/// </summary>
		[ReadToEnd]
		[WireMember(3)]
		public byte[] PartialData { get; internal set; } = new byte[0]; //TODO: Idk why but for ReadToEnd we have to give it a default

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterGuildCardChunkResponsePayload()
			: base(GameNetworkOperationCode.BB_GUILDCARD_CHUNK_TYPE)
		{
			
		}
	}
}
