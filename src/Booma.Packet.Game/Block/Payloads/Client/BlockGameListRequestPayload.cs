using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Payload sent to request the game list for the block.
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.GAME_LIST_TYPE)]
	public sealed partial class BlockGameListRequestPayload : PSOBBGamePacketPayloadClient
	{
		/// <summary>
		/// Empty packet, just requires the game list.
		/// </summary>
		public BlockGameListRequestPayload()
			: base(GameNetworkOperationCode.GAME_LIST_TYPE)
		{
			
		}
	}
}
