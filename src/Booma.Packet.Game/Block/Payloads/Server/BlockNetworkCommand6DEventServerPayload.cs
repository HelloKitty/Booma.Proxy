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
	/// Sent by the server.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.GAME_COMMANDD_TYPE)]
	public class BlockNetworkCommand6DEventServerPayload : PSOBBGamePacketPayloadServer, ISub6DCommandContainer, IMessageContextIdentifiable
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
		public BlockNetworkCommand6DEventServerPayload(byte targetRemotePlayer, [JetBrains.Annotations.NotNull] BaseSubCommand6D command)
		{
			Command = command ?? throw new ArgumentNullException(nameof(command));
			TargetClientIndex = targetRemotePlayer;
		}

		protected BlockNetworkCommand6DEventServerPayload()
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
