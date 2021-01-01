using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Booma.Packets.Tests;

namespace Booma
{
	public class SharedPayloadAutomatedReflectionTestsClient : AutomatedReflectionTests<PSOBBGamePacketPayloadClient, SharedLoginRequest93Payload>
	{

	}

	public class SharedPayloadAutomatedReflectionTestsServer : AutomatedReflectionTests<PSOBBGamePacketPayloadServer, SharedLoginResponsePayload>
	{

	}
}
