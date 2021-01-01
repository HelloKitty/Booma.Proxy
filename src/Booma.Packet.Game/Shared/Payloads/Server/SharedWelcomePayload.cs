using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
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
		//TODO: Move this to crypto constants
		/// <summary>
		/// Represents the required size of the initialization vectors.
		/// </summary>
		public const int ENCRYPTION_VECTOR_SIZE = 48;

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
		[KnownSize(ENCRYPTION_VECTOR_SIZE)]
		[WireMember(3)]
		public byte[] ServerVector { get; internal set; }

		/// <summary>
		/// Decryption initialization vector
		/// for the server crypto.
		/// </summary>
		[KnownSize(ENCRYPTION_VECTOR_SIZE)]
		[WireMember(4)]
		public byte[] ClientVector { get; internal set; }

		public SharedWelcomePayload([NotNull] string copyrightMessage, 
			[NotNull] byte[] serverVector, 
			[NotNull] byte[] clientVector) 
			: this()
		{
			CopyrightMessage = copyrightMessage ?? throw new ArgumentNullException(nameof(copyrightMessage));
			ServerVector = serverVector ?? throw new ArgumentNullException(nameof(serverVector));
			ClientVector = clientVector ?? throw new ArgumentNullException(nameof(clientVector));

			ClientVector.AssertLengthExact(ENCRYPTION_VECTOR_SIZE);
			ServerVector.AssertLengthExact(ENCRYPTION_VECTOR_SIZE);
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public SharedWelcomePayload()
			: base(GameNetworkOperationCode.BB_WELCOME_TYPE)
		{
			
		}
	}
}
