using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//https://github.com/Sylverant/ship_server/blob/da29a8e0ffbb394bd7cad462024e68df3909d528/src/packets.h#L537
	/// <summary>
	/// Sent by the PSOBB server when a player leaves a game session.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.GAME_LEAVE_TYPE)]
	public sealed partial class BlockOtherPlayerLeaveGameEventPayload : PSOBBGamePacketPayloadServer, IMessageContextIdentifiable
	{
		/// <summary>
		/// Indicates the ID of the player leaving the game.
		/// </summary>
		[WireMember(1)]
		public byte Identifier { get; internal set; }
		
		/// <summary>
		/// Indicates the ID of the player that has been promoted to the leader.
		/// </summary>
		[WireMember(2)]
		public byte PromotedLeaderIdentifier { get; internal set; }

		//TODO: Ctor for serverside
		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public BlockOtherPlayerLeaveGameEventPayload()
			: base(GameNetworkOperationCode.GAME_LEAVE_TYPE)
		{
			
		}
	}
}
