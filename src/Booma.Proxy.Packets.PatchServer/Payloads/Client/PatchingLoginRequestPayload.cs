using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/* The Login packet which contains the user's username/password.
	typedef struct patch_login
	{
		pkt_header_t hdr;
		uint8_t padding1[12];    //All zeroes
		char username[16];
		char password[16];
		uint8_t padding2[64];    //All zeroes
	}
	PACKED patch_login_pkt;*/

	//Syl struct: https://github.com/Sylverant/patch_server/blob/master/src/patch_packets.h#L62
	/// <summary>
	/// The login request packet for the patching server.
	/// </summary>
	[WireDataContract]
	[PatchClientPacketPayload(PatchNetworkOperationCode.PATCH_LOGIN_TYPE)]
	public sealed class PatchingLoginRequestPayload : PSOBBPatchPacketPayloadClient
	{
		/// <summary>
		/// Username to authenticate with the patchserver.
		/// </summary>
		[KnownSize(16)]
		[WireMember(2)]
		public string UserName { get; }

		/// <summary>
		/// Password to authenticate with the patchserver.
		/// </summary>
		[KnownSize(16)]
		[WireMember(3)]
		public string Password { get; }

		/// <summary>
		/// Padding (?)
		/// </summary>
		[KnownSize(64)]
		[WireMember(1)]
		private byte[] Padding2 { get; } = new byte[64];

		/// <summary>
		/// Padding (?)
		/// </summary>
		[KnownSize(12)]
		[WireMember(4)]
		private byte[] Padding { get; } = new byte[12];

		//serializer ctor

		public PatchingLoginRequestPayload([NotNull] string userName, [NotNull] string password)
		{
			if(string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(userName));
			if(string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

			//TODO: Verify length, I have a headache right now.

			UserName = userName;
			Password = password;
		}

		private PatchingLoginRequestPayload()
		{
			
		}
	}
}
