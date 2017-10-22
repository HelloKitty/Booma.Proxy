using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload the client sends when it has
	/// finished bursting into a party/game.
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.DONE_BURSTING_TYPE)]
	public sealed class BlockFinishedGameBurstingRequestPayload : PSOBBGamePacketPayloadClient
	{
		//This is just an empty payload that says we're done

		public BlockFinishedGameBurstingRequestPayload()
		{
			
		}
	}
}
