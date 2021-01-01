using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Payload that contains an info message.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.INFO_REPLY_TYPE)]
	public sealed partial class SharedInfoReplyEventPayload : PSOBBGamePacketPayloadServer
	{
		//TODO: What is this?
		[WireMember(1)]
		internal ulong unused { get; set; }

		/// <summary>
		/// The info message.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[WireMember(2)]
		public string Message { get; internal set; }

		public SharedInfoReplyEventPayload([NotNull] string message) 
			: this()
		{
			Message = message ?? throw new ArgumentNullException(nameof(message));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public SharedInfoReplyEventPayload()
			: base(GameNetworkOperationCode.INFO_REPLY_TYPE)
		{
			
		}
	}
}
