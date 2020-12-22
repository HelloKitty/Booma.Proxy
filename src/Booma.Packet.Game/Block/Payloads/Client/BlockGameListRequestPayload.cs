using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload sent to request the game list for the block.
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.GAME_LIST_TYPE)]
	public sealed class BlockGameListRequestPayload : PSOBBGamePacketPayloadClient
	{
		//Empty packet, just requires the game list

		public BlockGameListRequestPayload()
		{
			
		}
	}
}
