using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//Tethella: https://github.com/justnoxx/psobb-tethealla/blob/master/ship_server/ship_server.c#L2720
	/// <summary>
	/// Sent before a client begins to load and warp to a new
	/// area/map/level.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.BurstType5)]
	public sealed class Sub60ClientBurstBeginEventCommand : BaseSubCommand60
	{
		//TODO: This are about 518 bytes here for quest data
		[ReadToEnd]
		[WireMember(1)]
		private byte[] QuestData { get; } = new byte[0];

		//Serializer ctor
		private Sub60ClientBurstBeginEventCommand()
		{
			
		}
	}
}
