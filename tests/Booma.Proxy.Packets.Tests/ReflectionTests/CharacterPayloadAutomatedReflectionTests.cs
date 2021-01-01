using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Booma.Packets.Tests;

namespace Booma
{
	public class CharacterPayloadAutomatedReflectionTestsClient : AutomatedReflectionTests<PSOBBGamePacketPayloadClient, CharacterCharacterSelectionRequestPayload>
	{

	}

	public class CharacterPayloadAutomatedReflectionTestsServer : AutomatedReflectionTests<PSOBBGamePacketPayloadServer, CharacterCharacterUpdateResponsePayload>
	{

	}
}
