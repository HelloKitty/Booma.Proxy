using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/*typedef struct bb_subcmd_pkt {
		bb_pkt_hdr_t hdr;
		uint8_t type;
		uint8_t size;
		uint8_t data[0];
	} PACKED bb_subcmd_pkt_t;*/

	//Syl: https://github.com/Sylverant/ship_server/blob/b3bffc84b558821ca2002775ab2c3af5c6dde528/src/subcmd.h#L47

	/// <summary>
	/// Payload that is a container for opcode based network commands.
	/// Sent by the server.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.GAME_COMMAND0_TYPE)]
	public partial class BlockNetworkCommand60EventServerPayload : PSOBBGamePacketPayloadServer, ISub60CommandContainer
	{
		/// <summary>
		/// The subcommand.
		/// </summary>
		[WireMember(1)]
		public BaseSubCommand60 Command { get; internal set; }

		/// <inheritdoc />
		public BlockNetworkCommand60EventServerPayload(BaseSubCommand60 command)
			: this()
		{
			Command = command ?? throw new ArgumentNullException(nameof(command));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public BlockNetworkCommand60EventServerPayload()
			: base(GameNetworkOperationCode.GAME_COMMAND0_TYPE)
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			if(Command != null)
				return Command.ToString();

			return base.ToString();
		}
	}
}
