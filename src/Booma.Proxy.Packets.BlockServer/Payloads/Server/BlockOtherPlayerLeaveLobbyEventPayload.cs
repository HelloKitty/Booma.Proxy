using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload sent when another player leaves the lobby.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.LOBBY_LEAVE_TYPE)]
	public sealed class BlockOtherPlayerLeaveLobbyEventPayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// The ID of the client leaving.
		/// </summary>
		[WireMember(1)]
		public byte ClientId { get; }

		[WireMember(2)]
		public byte LeaderId { get; }

		//TODO: What is this?
		//uint16_t padding;

		//Serializer ctor
		private BlockOtherPlayerLeaveLobbyEventPayload()
		{
			
		}
	}
}
