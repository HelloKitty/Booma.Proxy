using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/* The Welcome packet for setting up encryption keys.
	typedef struct patch_welcome
	{
		pkt_header_t hdr;
		char copyright[44];      //Copyright message, see below.
		uint8_t padding[20];     //all zeroes
		uint32_t server_vector;
		uint32_t client_vector;
	}
	PACKED patch_welcome_pkt;*/

	//Syl sent: LOGIN_93_TYPE https://github.com/Sylverant/login_server/blob/master/src/bblogin.c#L121
	//Syl struct: https://github.com/Sylverant/patch_server/blob/1616d93cc653703e3787c246dfb7aaa8ef3044b1/src/patch_packets.c#L102
	/// <summary>
	/// The welcome message the patch server sends when you connect
	/// initially.
	/// </summary>
	[WireDataContract]
	[WireDataContractBaseLink(0x02, typeof(PSOBBPatchPacketPayloadServer))]
	public sealed class PatchingWelcomePayload : PSOBBPatchPacketPayloadServer
	{
		/// <summary>
		/// Copyright message sent down from the patch server.
		/// Always the same message.
		/// </summary>
		[KnownSize(44)]
		[WireMember(1)]
		public string PatchCopyrightMessage { get; } //I don't think this is null terminated?

		//TODO: Why?
		[KnownSize(20)]
		[WireMember(2)]
		private byte[] Padding { get; }

		//TODO: What is this?
		/// <summary>
		/// Server IV (?)
		/// </summary>
		[WireMember(3)]
		public uint ServerVector { get; }

		/// <summary>
		/// Client IV (?)
		/// </summary>
		[WireMember(4)]
		public uint ClientVector { get; }

		public PatchingWelcomePayload([NotNull] string patchCopyrightMessage, uint serverVector, uint clientVector)
		{
			if(string.IsNullOrWhiteSpace(patchCopyrightMessage)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(patchCopyrightMessage));

			PatchCopyrightMessage = patchCopyrightMessage;
			ServerVector = serverVector;
			ClientVector = clientVector;
		}

		//serializer ctor
		private PatchingWelcomePayload()
		{
			
		}
	}
}
