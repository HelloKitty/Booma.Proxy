using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//TODO: We may need a better name.
	/// <summary>
	/// Command sent the client when it should begin the bursting
	/// to a game that it is attempting to join.
	/// </summary>
	[WireDataContract]
	[SubCommand62(SubCommand62OperationCode.BurstType5)]
	public sealed class Sub62ClientBurstBeginEventCommand : BaseSubCommand62
	{
		//TODO: Is it really quest data like the Sub60 version?
		//TODO: This are about 518 bytes here for quest data
		[ReadToEnd]
		[WireMember(1)]
		internal byte[] QuestData { get; set; } = new byte[0];

		//Serializer ctor
		private Sub62ClientBurstBeginEventCommand()
		{
			
		}
	}
}
