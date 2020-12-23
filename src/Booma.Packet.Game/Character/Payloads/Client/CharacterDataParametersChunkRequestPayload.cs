using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using Reinterpret.Net;

namespace Booma.Proxy
{
	/// <summary>
	/// Command packet that tells the
	/// server to send the next file. File information can be queried for
	/// with <see cref="CharacterDataParametersHeaderRequestPayload"/>.
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.BB_PARAM_CHUNK_REQ_TYPE)]
	public sealed class CharacterDataParametersChunkRequestPayload : PSOBBGamePacketPayloadClient, IChunkRequest
	{
		//We don't want the flags to get the 4 byte ChunkNumber.
		/// <inheritdoc />
		public override bool isFlagsSerialized { get; } = false;

		//Do not add WireMember to this field. It is stored in flags
		//This value is stored in flags for some reason
		//Empty command packet. Contains no data just tells the server to send the next file.
		/// <summary>
		/// The file number to request
		/// </summary>
		[WireMember(1)]
		public uint ChunkNumber { get; internal set; }

		public CharacterDataParametersChunkRequestPayload(uint chunkNumber)
			: this()
		{
			ChunkNumber = chunkNumber;
		}

		//Serializer ctor
		private CharacterDataParametersChunkRequestPayload()
			: base(GameNetworkOperationCode.BB_PARAM_CHUNK_REQ_TYPE)
		{
			
		}
	}
}
