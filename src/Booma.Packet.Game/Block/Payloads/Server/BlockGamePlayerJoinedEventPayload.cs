using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	//See: https://github.com/Sylverant/ship_server/blob/9373df882859b234bc3e299d2e85f7b4c515d025/src/packets.h#L508
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.GAME_ADD_PLAYER_TYPE)]
	public sealed partial class BlockGamePlayerJoinedEventPayload : PSOBBGamePacketPayloadServer, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		//TODO: There is waaaay more here, but we don't have time to implement this right now.
		//See: https://github.com/Sylverant/ship_server/blob/9373df882859b234bc3e299d2e85f7b4c515d025/src/packets.h#L508

		/// <inheritdoc />
		public BlockGamePlayerJoinedEventPayload(byte identifier)
			: this()
		{
			Identifier = identifier;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public BlockGamePlayerJoinedEventPayload()
			: base(GameNetworkOperationCode.GAME_ADD_PLAYER_TYPE)
		{
			
		}
	}
}
