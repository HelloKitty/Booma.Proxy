using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Challenge mode rank data.
	/// Sylverant does not send this to BB.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.C_RANK_TYPE)]
	public sealed partial class ChallengeModeRankDataEventPayload : PSOBBGamePacketPayloadServer
	{
		//TODO: Implement.
		public ChallengeModeRankDataEventPayload() 
			: base(GameNetworkOperationCode.C_RANK_TYPE)
		{

		}
	}
}
