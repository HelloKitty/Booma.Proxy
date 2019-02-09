using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	[SubCommand6D(SubCommand6DOperationCode.PlayerJoinedData)]
	public sealed class Sub6DFakePlayerJoinDataNeededCommand : BaseSubCommand6D
	{
		//See: https://github.com/justnoxx/psobb-tethealla/blob/master/ship_server/ship_server.c#L11090
		//This is a lot of different types of data here
		//the issue is, I don't know how long it's actually suppose to be.
		[KnownSize(0x4C0 - 8)]
		[WireMember(2)]
		public byte[] UnknownBytes { get; } = new byte[0x4C0 - 8];

		public Sub6DFakePlayerJoinDataNeededCommand()
		{
			CommandSize = 0x4C0;
		}
	}
}
