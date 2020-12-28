using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Response to the <see cref="CharacterChecksumRequestPayload"/>.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BB_CHECKSUM_ACK_TYPE)]
	public sealed partial class CharacterChecksumResponsePayload : PSOBBGamePacketPayloadServer, IResponseCodePayload<LoginChecksumResult>
	{
		/// <inheritdoc />
		[WireMember(1)]
		public LoginChecksumResult ResponseCode { get; internal set; }

		public CharacterChecksumResponsePayload(LoginChecksumResult responseCode) 
			: this()
		{
			if(!Enum.IsDefined(typeof(LoginChecksumResult), responseCode)) throw new ArgumentOutOfRangeException(nameof(responseCode), "Value should be defined in the LoginChecksumResult enum.");

			ResponseCode = responseCode;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterChecksumResponsePayload()
			: base(GameNetworkOperationCode.BB_CHECKSUM_ACK_TYPE)
		{
			
		}
	}
}
