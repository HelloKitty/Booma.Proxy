using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
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
	/// Sent by the client.
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.GAME_COMMANDD_TYPE)]
	public class BlockNetworkCommand6DEventClientPayload : PSOBBGamePacketPayloadClient, ISub6DCommandContainer
	{
		//Sub6D actually uses the flags for something.
		//It uses the first flags byte to indicate which remote player index
		//the 6D messsage should be sent to.
		/// <inheritdoc />
		public override bool isFlagsSerialized => false;

		[WireMember(1)]
		internal int TargetClientIndex { get; set; }

		/// <inheritdoc />
		public byte Identifier => (byte)TargetClientIndex;

		/// <summary>
		/// The subcommand.
		/// </summary>
		[WireMember(2)]
		public BaseSubCommand6D Command { get; internal set; }

		/// <inheritdoc />
		public BlockNetworkCommand6DEventClientPayload(byte targetRemotePlayer, [NotNull] BaseSubCommand6D command)
			: this()
		{
			Command = command ?? throw new ArgumentNullException(nameof(command));
			TargetClientIndex = targetRemotePlayer;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public BlockNetworkCommand6DEventClientPayload()
			: base(GameNetworkOperationCode.GAME_COMMANDD_TYPE)
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
