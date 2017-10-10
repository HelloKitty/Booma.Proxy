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
	/// with <see cref="LoginDataParametersHeaderRequestPayload"/>.
	/// </summary>
	[WireDataContract]
	[LoginClientPacketPayload(LoginNetworkOperationCodes.BB_PARAM_CHUNK_REQ_TYPE)]
	public sealed class LoginDataParametersChunkRequestPayload : PSOBBLoginPacketPayloadClient, IChunkRequest
	{
		//Do not add WireMember to this field. It is stored in flags
		//This value is stored in flags for some reason
		//Empty command packet. Contains no data just tells the server to send the next file.
		/// <summary>
		/// The file number to request
		/// </summary>
		public uint ChunkNumber => Flags.Reinterpret<uint>(); //the chunk number is stored in flags so reinterpret

		public LoginDataParametersChunkRequestPayload(uint chunkNumber)
			: base(chunkNumber.Reinterpret()) //cast to bytes
		{

		}

		//Serializer ctor
		private LoginDataParametersChunkRequestPayload()
		{
			
		}
	}
}
