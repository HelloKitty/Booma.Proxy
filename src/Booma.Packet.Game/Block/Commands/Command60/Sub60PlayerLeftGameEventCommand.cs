using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//Syl: https://github.com/Sylverant/ship_server/blob/b3bffc84b558821ca2002775ab2c3af5c6dde528/src/packets.h#L537
	/// <summary>
	/// Payload sent when another player leaves the game.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.GAME_LEAVE_TYPE)]
	public sealed class Sub60PlayerLeftGameEventCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/*uint8_t client_id;
		uint8_t leader_id;
		uint16_t padding;*/
		
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		//TODO: Why do we need this?
		[WireMember(2)]
		internal byte LeaderId { get; set; }

		//TODO: Is this really unused as Syl says?
		[WireMember(3)]
		internal short unk { get; set; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60PlayerLeftGameEventCommand()
			: base(SubCommand60OperationCode.GAME_LEAVE_TYPE)
		{
			
		}
	}
}
