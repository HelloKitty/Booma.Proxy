using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Booma.Packets.Tests;

namespace Booma
{
	public class BlockPayloadAutomatedReflectionTestsClient : AutomatedReflectionTests<PSOBBGamePacketPayloadClient, BlockNetworkCommand60EventClientPayload>
	{

	}

	public class BlockPayloadAutomatedReflectionTestsServer : AutomatedReflectionTests<PSOBBGamePacketPayloadServer, BlockCharacterDataInitializationServerRequestPayload>
	{

	}
}
