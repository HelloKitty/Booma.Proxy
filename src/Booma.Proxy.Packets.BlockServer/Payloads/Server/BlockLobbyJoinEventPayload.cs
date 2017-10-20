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

		[WireMember(2)]
		public byte LeaderId { get; }

		[WireMember(3)]
		private byte One { get; } //one for some reason?

		/// <summary>
		/// The number of the lobby being joined.
		/// </summary>
		[WireMember(4)]
		public byte LobbyNumber { get; }

		//TODO: There is more to the packet here: https://github.com/Sylverant/ship_server/blob/b3bffc84b558821ca2002775ab2c3af5c6dde528/src/packets.h#L517

		private BlockLobbyJoinEventPayload()
		{
			
		}
	}
}
