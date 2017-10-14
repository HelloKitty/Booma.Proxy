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
	[WireDataContract(WireDataContractAttribute.KeyType.Byte, InformationHandlingFlags.DontConsumeRead, true)] //sent as a byte. Runtime linking SHOULD work since it'll find subtypes in its search for payloads
	[DefaultChild(typeof(UnknownSubCommand60ServerPayload))] //if we encounter ones we don't know then we should produce this payload.
	[GameServerPacketPayload(GameNetworkOperationCode.GAME_COMMAND0_TYPE)]
	public abstract class BlockNetworkCommandEventServerPayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// Indicates if the <see cref="Size"/> property is serialized and
		/// deserialized.
		/// Child Types can override this to gain access to the single Byte size if needed.
		/// </summary>
		public virtual bool isSizeSerialized { get; } = true;

		/// <summary>
		/// The operation code for the subcommand.
		/// This is only read for logging of unknown subcommands.
		/// </summary>
		[WireMember(1)]
		protected SubCommand60OperationCode SubcommandOperationCode { get; }

		//Since the Type byte is eaten by the polymorphic deserialization process
		//We just read t he size to discard it
		/// <summary>
		/// The size of the subcommand (subpayload).
		/// Not needed for deserialization of subcommand.
		/// </summary>
		[Optional(nameof(isSizeSerialized))]
		[WireMember(2)]
		private byte Size { get; }

		protected BlockNetworkCommandEventServerPayload()
		{
			
		}
	}
}
