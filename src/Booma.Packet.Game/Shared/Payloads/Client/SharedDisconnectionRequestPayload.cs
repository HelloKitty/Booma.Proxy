using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// A request to disconnect from the server.
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.TYPE_05)]
	public sealed partial class SharedDisconnectionRequestPayload : PSOBBGamePacketPayloadClient
	{
		//Empty command payload. Doesn't send any data.
		public SharedDisconnectionRequestPayload()
			: base(GameNetworkOperationCode.TYPE_05)
		{
			
		}
	}
}
