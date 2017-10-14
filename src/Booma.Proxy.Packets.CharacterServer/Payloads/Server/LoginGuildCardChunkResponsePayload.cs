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
	public sealed class LoginGuildCardChunkResponsePayload : PSOBBGamePacketPayloadServer, IChunkResponse
	{
		//TODO: What is this?
		[WireMember(1)]
		private int unk { get; }

		/// <summary>
		/// The chunk number of this partial data.
		/// </summary>
		[WireMember(2)]
		public uint ChunkNumber { get; }

		//The maximum size
		/// <summary>
		/// This is a partial data chunk of the guild card
		/// data.
		/// See: https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/player.h#L272
		/// </summary>
		[ReadToEnd]
		[WireMember(3)]
		public byte[] PartialData { get; } = new byte[0]; //TODO: Idk why but for ReadToEnd we have to give it a default

		//Serializer ctor
		private LoginGuildCardChunkResponsePayload()
		{
			
		}
	}
}
