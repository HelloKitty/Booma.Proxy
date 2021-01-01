using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma;
using FreecraftCore.Serializer;

namespace Booma
{
	[WireDataContract]
	public sealed class LobbyMenuEntry : MenuItemIdentifier
	{
		//TODO: Maybe this is flags for menu stuff?? Like full?
		//See: https://github.com/Sylverant/ship_server/blob/4b94e90d9857fb88f45537f25fa589f1b5d90bda/src/ship_packets.c#L1165
		[WireMember(1)]
		internal int Padding { get; set; } = 0;

		/// <inheritdoc />
		public LobbyMenuEntry(uint menuId, uint itemId)
			: base(menuId, itemId)
		{

		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public LobbyMenuEntry()
		{
			
		}
	}

	/// <summary>
	/// Packet sent by a block initially that contains all the menu information for a lobbies.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.LOBBY_LIST_TYPE)]
	public sealed partial class LobbyListEventPayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// Flags sends the Lobby List size.
		/// </summary>
		public override bool isFlagsSerialized { get; } = false;

		/// <summary>
		/// Length-prefixed serialized menu identifiers sent with 4 byte padding
		/// that represent lobby menu options.
		/// </summary>
		[SendSize(PrimitiveSizeType.Int32)]
		[WireMember(1)]
		internal LobbyMenuEntry[] _Lobbies { get; set; }

		/// <summary>
		/// The lobby menu entries.
		/// </summary>
		public IEnumerable<MenuItemIdentifier> Lobbies => _Lobbies;

		//Sylverant claims: /* There's padding at the end -- enough for one more lobby. */
		[KnownSize(sizeof(int) * 3)]
		[WireMember(2)]
		internal byte[] Padding { get; set; } = Array.Empty<byte>();

		public LobbyListEventPayload(LobbyMenuEntry[] lobbies) 
			: this()
		{
			_Lobbies = lobbies ?? throw new ArgumentNullException(nameof(lobbies));
		}

		public LobbyListEventPayload(MenuItemIdentifier[] lobbies)
			: this()
		{
			if (lobbies == null) throw new ArgumentNullException(nameof(lobbies));

			_Lobbies = lobbies
				.Select(l => new LobbyMenuEntry(l.MenuId, l.ItemId))
				.ToArray();
		}

		public LobbyListEventPayload() 
			: base(GameNetworkOperationCode.LOBBY_LIST_TYPE)
		{

		}
	}
}
