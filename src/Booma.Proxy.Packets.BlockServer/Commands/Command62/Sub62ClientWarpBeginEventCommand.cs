using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Command sent the client when it should begin the bursting
	/// to a game that it is attempting to join.
	/// </summary>
	[WireDataContract]
	[SubCommand62(SubCommand62OperationCode.BurstType5)]
	public sealed class Sub62ClientWarpBeginEventCommand : BaseSubCommand62
	{
		//TODO: Is it really quest data like the Sub60 version?
		//TODO: This are about 518 bytes here for quest data
		[ReadToEnd]
		[WireMember(1)]
		private byte[] QuestData { get; } = new byte[0];

		//Serializer ctor
		private Sub62ClientWarpBeginEventCommand()
		{
			
		}
	}
}
