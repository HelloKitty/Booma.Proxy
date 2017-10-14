using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload the server sends when it wants the client to send
	/// <see cref="BlockCharacterDataInitializeClientResponsePayload"/>.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.CHAR_DATA_REQUEST_TYPE)]
	public sealed class BlockCharacterDataInitializationServerRequestPayload : PSOBBGamePacketPayloadServer
	{
		//Empty, just a command payload that tells the client to send character init.

		public BlockCharacterDataInitializationServerRequestPayload()
		{
			
		}
	}
}
