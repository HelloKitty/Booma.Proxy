using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Booma.Proxy.Packets.Tests;

namespace Booma.Proxy
{
	//TODO: Enable when the we implement client payloads for ship
	/*public class ShipPayloadAutomatedReflectionTestsClient : AutomatedReflectionTests<PSOBBLoginPacketPayloadClient, LoginLoginRequest93Payload>
	{

	}*/

	public class ShipPayloadAutomatedReflectionTestsServer : AutomatedReflectionTests<PSOBBGamePacketPayloadServer, LoginBlockListEventPayload>
	{

	}
}
