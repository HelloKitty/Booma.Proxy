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
	public sealed class CharacterChecksumResponsePayload : PSOBBGamePacketPayloadServer, IResponseCodePayload<LoginChecksumResult>
	{
		/// <inheritdoc />
		[WireMember(1)]
		public LoginChecksumResult ResponseCode { get; }

		//Serializer ctor
		private CharacterChecksumResponsePayload()
		{
			
		}
	}
}
