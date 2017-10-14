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
	/// Login server payload responsible for providing the encryption IVs for the
	/// blowfish encryption.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BB_WELCOME_TYPE)]
	public sealed class LoginWelcomePayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// Copyright message that the server sends to
		/// the client which it verifies.
		/// </summary>
		[KnownSize(96)]
		[WireMember(2)]
		public string CopyrightMessage { get; }

		/// <summary>
		/// Encryption initialization vector
		/// for the login server crypto.
		/// </summary>
		[KnownSize(48)]
		[WireMember(3)]
		public byte[] ServerVector { get; }

		/// <summary>
		/// Decryption initialization vector
		/// for the login server crypto.
		/// </summary>
		[KnownSize(48)]
		[WireMember(4)]
		public byte[] ClientVector { get; }

		//Serializer ctor
		private LoginWelcomePayload()
		{
			
		}
	}
}
