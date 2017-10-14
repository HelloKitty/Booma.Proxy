using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Packet sent to the client telling it to join a lobby.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.LOBBY_JOIN_TYPE)]
	public sealed class BlockLobbyJoinEventPayload : PSOBBGamePacketPayloadServer
	{
		//TODO: We can't currently handle this packet. It does something odd the serializer can't handle
		//For now we read up until clientid
		[WireMember(1)]
		public byte ClientId { get; }

		private BlockLobbyJoinEventPayload()
		{
			
		}
	}
}
