using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Booma.Proxy.Packets.Tests;

namespace Booma.Proxy
{
	public class CharacterPayloadAutomatedReflectionTestsClient : AutomatedReflectionTests<PSOBBLoginPacketPayloadClient, LoginCharacterSelectionRequestPayload>
	{

	}

	public class CharacterPayloadAutomatedReflectionTestsServer : AutomatedReflectionTests<PSOBBLoginPacketPayloadServer, LoginCharacterUpdateResponsePayload>
	{

	}
}
