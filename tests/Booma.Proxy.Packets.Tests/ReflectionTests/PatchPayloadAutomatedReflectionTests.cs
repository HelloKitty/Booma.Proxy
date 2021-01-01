using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Booma.Packets.Tests;

namespace Booma
{
	public class PatchPayloadAutomatedReflectionTestsServer : AutomatedReflectionTests<PSOBBPatchPacketPayloadServer, PatchingDoneCommandPayload>
	{

	}

	public class PatchPayloadAutomatedReflectionTestsClient : AutomatedReflectionTests<PSOBBPatchPacketPayloadClient, PatchingLoginRequestPayload>
	{

	}
}
