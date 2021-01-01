using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	//Tethella: https://github.com/justnoxx/psobb-tethealla/blob/master/ship_server/ship_server.c#L2720
	/// <summary>
	/// Sent before a client begins to load and warp to a new
	/// area/map/level.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.BurstType5)]
	public sealed partial class Sub60ClientBurstBeginEventCommand : BaseSubCommand60
	{
		//TODO: This are about 518 bytes here for quest data
		[KnownSize(522)] //I Now think this is 522?? But really hard to tell honestly.
		[WireMember(1)]
		public byte[] QuestData { get; internal set; } = Array.Empty<byte>();

		public Sub60ClientBurstBeginEventCommand([NotNull] byte[] questData)
			: this()
		{
			QuestData = questData ?? throw new ArgumentNullException(nameof(questData));
			CommandSize = (byte) ((COMMAND_HEADER_SIZE + QuestData.Length) / 4);
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60ClientBurstBeginEventCommand()
			: base(SubCommand60OperationCode.BurstType5)
		{
			
		}
	}
}
