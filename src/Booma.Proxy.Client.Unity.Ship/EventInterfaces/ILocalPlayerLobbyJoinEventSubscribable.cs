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

	public sealed class LobbyJoinedEventArgs : EventArgs
	{
		/// <summary>
		/// The ID of the lobby loaded.
		/// </summary>
		public int LobbyId { get; }

		/// <inheritdoc />
		public LobbyJoinedEventArgs(int lobbyId)
		{
			if(lobbyId < 0) throw new ArgumentOutOfRangeException(nameof(lobbyId));

			LobbyId = lobbyId;
		}
	}
}
