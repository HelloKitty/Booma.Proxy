using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma
{
	/// <summary>
	/// Packet sent by the client to request a lobby change.
	/// The server responds with 95 or 01.
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.LOBBY_CHANGE_TYPE)]
	public sealed partial class BlockLobbyChangeRequestPayload : PSOBBGamePacketPayloadClient
	{
		/// <summary>
		/// The menu selection involved with the lobby change
		/// request.
		/// </summary>
		[WireMember(1)]
		public MenuItemIdentifier Selection { get; internal set; }

		/// <inheritdoc />
		public BlockLobbyChangeRequestPayload([NotNull] MenuItemIdentifier selection)
			: this()
		{
			Selection = selection ?? throw new ArgumentNullException(nameof(selection));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public BlockLobbyChangeRequestPayload()
			: base(GameNetworkOperationCode.LOBBY_CHANGE_TYPE)
		{
			
		}
	}
}
