using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/* Blue Burst packet that acts as a header for the client's guildcard data.
	typedef struct bb_guildcard_hdr
	{
		bb_pkt_hdr_t hdr;
		uint8_t one;
		uint8_t padding1[3];
		uint16_t len;
		uint8_t padding2[2];
		uint32_t checksum;
	}
	PACKED bb_guildcard_hdr_pkt;*/

	/// <summary>
	/// Payload sent as a response to <see cref="LoginGuildRequestPayload"/>.
	/// Contains initial information about the guild info.
	/// </summary>
	[WireDataContract]
	[LoginServerPacketPayload(LoginNetworkOperationCodes.BB_GUILDCARD_HEADER_TYPE)]
	public sealed class LoginGuildCardDataHeaderResponsePayload : PSOBBLoginPacketPayloadServer
	{
		/// <summary>
		/// One? Not sure why.
		/// </summary>
		[WireMember(1)]
		private int One { get; } = 1;

		//TODO: What is this length?
		public uint Length { get; }

		//TODO: What is this checksum?
		public uint CheckSum { get; }

		//Serializer ctor
		private LoginGuildCardDataHeaderResponsePayload()
		{
			
		}
	}
}
