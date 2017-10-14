using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Booma.Proxy.Packets.Tests;

namespace Booma.Proxy
{
	public class SharedPayloadAutomatedReflectionTestsClient : AutomatedReflectionTests<PSOBBLoginPacketPayloadClient, LoginLoginRequest93Payload>
	{

	}

	public class SharedPayloadAutomatedReflectionTestsServer : AutomatedReflectionTests<PSOBBLoginPacketPayloadServer, LoginLoginResponsePayload>
	{

	}
}
