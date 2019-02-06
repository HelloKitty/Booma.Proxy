using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface ILocalPlayerLobbyJoinEventSubscribable
	{
		event EventHandler<LobbyJoinedEventArgs> OnLocalPlayerLobbyJoined;
	}

	public sealed class LobbyJoinedEventArgs : EventArgs, IEntityIdentifable
	{
		/// <summary>
		/// The ID of the lobby loaded.
		/// </summary>
		public int LobbyId { get; }

		/// <inheritdoc />
		public int EntityGuid { get; }

		/// <inheritdoc />
		public LobbyJoinedEventArgs(int lobbyId, int entityGuid)
		{
			if(lobbyId < 0) throw new ArgumentOutOfRangeException(nameof(lobbyId));

			//TODO: Validate entity guid

			LobbyId = lobbyId;
			EntityGuid = entityGuid;
		}
	}
}
