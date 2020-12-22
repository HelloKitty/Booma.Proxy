using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//Syl: https://github.com/Sylverant/ship_server/blob/b3bffc84b558821ca2002775ab2c3af5c6dde528/src/packets.h#L898
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.GAME_LIST_TYPE)]
	public sealed class BlockGameListResponsePayload : PSOBBGamePacketPayloadServer
	{
		//This packet uses the flags for the length of the entries array - 1
		/// <inheritdoc />
		public override bool isFlagsSerialized => false;

		/// <summary>
		/// The collection of game/party listings.
		/// Contains information about the party too.
		/// </summary>
		[SendSize(SendSizeAttribute.SizeType.Int32, 1)] //sends it size - 1 so we need to account for that
		[WireMember(1)]
		internal GameListEntry[] _GameEntries { get; set; }

		/// <summary>
		/// The collection of game/party listings.
		/// </summary>
		public IEnumerable<GameListEntry> GameEntries => _GameEntries?.Skip(1); //first one is junk

		//Serializer ctor
		private BlockGameListResponsePayload()
		{
			
		}
	}
}
