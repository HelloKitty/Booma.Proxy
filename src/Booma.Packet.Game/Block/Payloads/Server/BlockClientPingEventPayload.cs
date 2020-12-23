using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//https://github.com/Sylverant/ship_server/blob/b3bffc84b558821ca2002775ab2c3af5c6dde528/src/ship.c#L206
	/// <summary>
	/// Sent by the PSOBB server. Must be responded too or the client will eventually
	/// be timed out for non-response.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.PING_TYPE)]
	public sealed class BlockClientPingEventPayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// Empty payload that tells the client to send a ping response.
		/// </summary>
		public BlockClientPingEventPayload()
			: base(GameNetworkOperationCode.PING_TYPE)
		{
			
		}
	}
}
