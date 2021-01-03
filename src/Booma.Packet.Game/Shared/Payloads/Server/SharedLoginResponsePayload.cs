using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma
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

	[WireDataContract]
	public sealed class SecurityData
	{
		//This doesn't seem to be anything special?
		[WireMember(1)]
		public uint Magic { get; internal set; } = 0x69;

		[WireMember(2)]
		public byte Slot { get; internal set; }

		[WireMember(3)]
		public bool SelectedCharacter { get; internal set; }

		[KnownSize(34)]
		[WireMember(3)]
		public byte[] Unknown { get; internal set; } = Array.Empty<byte>();

		public SecurityData(byte slot, bool selectedCharacter)
		{
			Slot = slot;
			SelectedCharacter = selectedCharacter;
		}

		public SecurityData(byte slot, bool selectedCharacter, byte[] unknown)
		{
			Slot = slot;
			SelectedCharacter = selectedCharacter;
			Unknown = unknown ?? throw new ArgumentNullException(nameof(unknown));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public SecurityData()
		{

		}
	}

	/* Fill in the information
	pkt->err_code = LE32(err);
	pkt->tag = LE32(0x00010000);
	pkt->guildcard = LE32(gc);
	pkt->team_id = LE32(team);
	pkt->caps = LE32(0x00000102);   /* ??? - newserv sets it this way */

	//Syl sent: BB_SECURITY_TYPE https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/login_packets.c#L319
	//Syl struct: https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/packets.h#L373
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BB_SECURITY_TYPE)]
	public sealed partial class SharedLoginResponsePayload : PSOBBGamePacketPayloadServer, IResponseCodePayload<AuthenticationResponseCode>, IResponseSucceedable
	{
		/// <inheritdoc />
		[WireMember(1)]
		public AuthenticationResponseCode ResponseCode { get; internal set; }

		/// <inheritdoc />
		public bool isSuccessful => ResponseCode == AuthenticationResponseCode.LOGIN_93BB_OK;

		//TODO: What is this?
		/// <summary>
		/// ?
		/// </summary>
		[WireMember(2)]
		public int Tag { get; internal set; } = 0x00010000;

		/// <summary>
		/// The account-wide guildcard number.
		/// </summary>
		[WireMember(3)]
		public uint GuildCard { get; internal set; }

		//it is easier to work with his as an int in .NET
		/// <summary>
		/// The account-wide team id.
		/// </summary>
		[WireMember(4)]
		public int TeamId { get; internal set; } 

		//TODO: What is this really?
		/// <summary>
		/// Client security information (?)
		/// </summary>
		[WireMember(5)]
		public SecurityData SecurityData { get; internal set; }

		//TODO: What is this?
		[WireMember(6)]
		public int Caps { get; internal set; } = 0x00000102; //newserv

		/// <summary>
		/// Creates a successful auth response with code <see cref="AuthenticationResponseCode"/> OK.
		/// </summary>
		/// <param name="guildCard">The guild card of the account.</param>
		/// <param name="teamId">The team id the account is asscoiated with.</param>
		/// <param name="securityData">The security data (?)</param>
		public SharedLoginResponsePayload(uint guildCard, int teamId, SecurityData securityData)
			: this()
		{
			GuildCard = guildCard;
			TeamId = teamId;
			SecurityData = securityData ?? throw new ArgumentNullException(nameof(securityData));
			ResponseCode = AuthenticationResponseCode.LOGIN_93BB_OK;
		}

		/// <summary>
		/// Creates a failed auth` response with a code other than <see cref="AuthenticationResponseCode"/> OK.
		/// </summary>
		/// <param name="responseCode">The response code to send.</param>
		public SharedLoginResponsePayload(AuthenticationResponseCode responseCode)
			: this()
		{
			if(!Enum.IsDefined(typeof(AuthenticationResponseCode), responseCode)) throw new InvalidEnumArgumentException(nameof(responseCode), (int)responseCode, typeof(AuthenticationResponseCode));
			if(responseCode == AuthenticationResponseCode.LOGIN_93BB_OK) throw new ArgumentException($"Cannot create failure response with Code: {responseCode}", nameof(responseCode));

			ResponseCode = responseCode;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public SharedLoginResponsePayload()
			: base(GameNetworkOperationCode.BB_SECURITY_TYPE)
		{
			
		}
	}
}
