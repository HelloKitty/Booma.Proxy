using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload that contains an info message.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.INFO_REPLY_TYPE)]
	public sealed class SharedInfoReplyEventPayload : PSOBBGamePacketPayloadServer
	{
		//TODO: What is this?
		[KnownSize(2)]
		[WireMember(1)]
		internal uint[] unused { get; set; }

		/// <summary>
		/// The info message.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[WireMember(2)]
		public string Message { get; internal set; }

		//Serializer ctor
		private SharedInfoReplyEventPayload()
		{
			
		}
	}
}
