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
	public sealed partial class CharacterGuildCardChunkResponsePayload : PSOBBGamePacketPayloadServer, IChunkResponse
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
		public byte[] PartialData { get; internal set; }

		public CharacterGuildCardChunkResponsePayload(uint chunkNumber, [NotNull] byte[] partialData)
			: this()
		{
			ChunkNumber = chunkNumber;
			PartialData = partialData ?? throw new ArgumentNullException(nameof(partialData));

			//Sylverant has a check here for size:
			//https://github.com/Sylverant/login_server/blob/4974a8891e7f273e8b0317932912d6f788c505c8/src/login_packets.c#L1982
			if (partialData.Length > 0x6800)
				throw new ArgumentException($"Provided data chunk too large. Length: {partialData.Length} Max: {0x6800}", nameof(partialData));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterGuildCardChunkResponsePayload()
			: base(GameNetworkOperationCode.BB_GUILDCARD_CHUNK_TYPE)
		{
			
		}
	}
}
