using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Payload the server sends when it wants the client to send
	/// <see cref="BlockCharacterDataInitializeClientResponsePayload"/>.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.CHAR_DATA_REQUEST_TYPE)]
	public sealed partial class BlockCharacterDataInitializationServerRequestPayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// Empty, just a command payload that tells the client to send character init.
		/// </summary>
		public BlockCharacterDataInitializationServerRequestPayload()
			: base(GameNetworkOperationCode.CHAR_DATA_REQUEST_TYPE)
		{
			
		}
	}
}
