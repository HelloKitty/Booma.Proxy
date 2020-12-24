using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/*typedef struct bb_timestamp {
		bb_pkt_hdr_t hdr;
		char timestamp[28];
	} PACKED bb_timestamp_pkt;*/

	//Syl: https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/packets.h#L349
	/// <summary>
	/// Payload event send by the server that contains
	/// a timestamp string.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.TIMESTAMP_TYPE)]
	public sealed partial class CharacterTimestampEventPayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// The time stamp.
		/// See Syl for format.
		/// </summary>
		[KnownSize(28)]
		[WireMember(1)]
		public string Timestamp { get; internal set; }

		public CharacterTimestampEventPayload([JetBrains.Annotations.NotNull] string timestamp) 
			: this()
		{
			Timestamp = timestamp ?? throw new ArgumentNullException(nameof(timestamp));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterTimestampEventPayload()
			: base(GameNetworkOperationCode.TIMESTAMP_TYPE)
		{
			
		}
	}
}
