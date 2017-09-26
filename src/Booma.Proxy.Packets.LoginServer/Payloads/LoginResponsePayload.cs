using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/*typedef struct bb_security
	{
		bb_pkt_hdr_t hdr;
		uint32_t err_code;
		uint32_t tag;
		uint32_t guildcard;
		uint32_t team_id;
		uint8_t security_data[40];
		uint32_t caps;
	}
	PACKED bb_security_pkt;*/

	/* Fill in the information
	pkt->err_code = LE32(err);
	pkt->tag = LE32(0x00010000);
	pkt->guildcard = LE32(gc);
	pkt->team_id = LE32(team);
	pkt->caps = LE32(0x00000102);   /* ??? - newserv sets it this way */

	//Syl sent: BB_SECURITY_TYPE https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/login_packets.c#L319
	//Syl struct: https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/packets.h#L373
	[WireDataContract]
	[WireDataContractBaseLink(0xE6, typeof(PSOBBShipPacketPayloadServer))] //TODO: Enumerate opcodes
	public sealed class LoginResponsePayload : PSOBBShipPacketPayloadServer, IResponseCodePayload<LoginResponseCode>, IResponseSucceedable
	{
		/// <inheritdoc />
		[WireMember(1)]
		public LoginResponseCode ResponseCode { get; }

		/// <inheritdoc />
		public bool isSuccessful => ResponseCode == LoginResponseCode.LOGIN_93BB_OK;

		//TODO: What is this?
		/// <summary>
		/// ?
		/// </summary>
		[WireMember(2)]
		public int Tag { get; } = 0x00010000;

		/// <summary>
		/// The account-wide guildcard number.
		/// </summary>
		[WireMember(3)]
		public uint GuildCard { get; }

		/// <summary>
		/// The account-wide team id.
		/// </summary>
		[WireMember(4)]
		public uint TeamId { get; }

		//TODO: What is this really?
		/// <summary>
		/// Client security information (?)
		/// </summary>
		[KnownSize(40)]
		[WireMember(5)]
		public byte[] SecurityData { get; }

		//TODO: What is this?
		[WireMember(6)]
		public int Caps { get; } = 0x00000102; //newserv

		/// <summary>
		/// Creates a successful login response with code <see cref="LoginResponseCode"/> OK.
		/// </summary>
		/// <param name="guildCard">The guild card of the account.</param>
		/// <param name="teamId">The team id the account is asscoiated with.</param>
		/// <param name="securityData">The security data (?)</param>
		public LoginResponsePayload(uint guildCard, uint teamId, byte[] securityData)
		{
			if(securityData == null) throw new ArgumentNullException(nameof(securityData));
			if(securityData.Length != 40) throw new ArgumentException("Security data must be 40 bytes. Use fail ctor if you want to not provide the data.", nameof(securityData));

			GuildCard = guildCard;
			TeamId = teamId;
			SecurityData = securityData;
			ResponseCode = LoginResponseCode.LOGIN_93BB_OK;
		}

		/// <summary>
		/// Creates a failed login response with a code other than <see cref="LoginResponseCode"/> OK.
		/// </summary>
		/// <param name="responseCode">The response code to send.</param>
		public LoginResponsePayload(LoginResponseCode responseCode)
		{
			if(!Enum.IsDefined(typeof(LoginResponseCode), responseCode)) throw new InvalidEnumArgumentException(nameof(responseCode), (int)responseCode, typeof(LoginResponseCode));
			if(responseCode == LoginResponseCode.LOGIN_93BB_OK) throw new ArgumentException($"Cannot create failure response with Code: {responseCode}", nameof(responseCode));

			//Must be non-null
			SecurityData = new byte[40];
			ResponseCode = responseCode;
		}

		private LoginResponsePayload()
		{
			
		}
	}
}
