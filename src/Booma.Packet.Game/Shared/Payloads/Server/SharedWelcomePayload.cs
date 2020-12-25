using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/*typedef struct bb_welcome
	{
		bb_pkt_hdr_t hdr;
		char copyright[0x60];
		uint8_t svect[48];
		uint8_t cvect[48];
	}
	PACKED bb_welcome_pkt;*/

	/// <summary>
	/// Shared server payload responsible for providing the encryption IVs for the
	/// blowfish encryption.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BB_WELCOME_TYPE)]
	public sealed partial class SharedWelcomePayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// Copyright message that the server sends to
		/// the client which it verifies.
		/// </summary>
		[DontTerminate] //exact length, terminator or not.
		[KnownSize(96)]
		[WireMember(2)]
		public string CopyrightMessage { get; internal set; }

		/// <summary>
		/// Encryption initialization vector
		/// for the server crypto.
		/// </summary>
		[KnownSize(48)]
		[WireMember(3)]
		public byte[] ServerVector { get; internal set; }

		/// <summary>
		/// Decryption initialization vector
		/// for the server crypto.
		/// </summary>
		[KnownSize(48)]
		[WireMember(4)]
		public byte[] ClientVector { get; internal set; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public SharedWelcomePayload()
			: base(GameNetworkOperationCode.BB_WELCOME_TYPE)
		{
			
		}
	}
}
