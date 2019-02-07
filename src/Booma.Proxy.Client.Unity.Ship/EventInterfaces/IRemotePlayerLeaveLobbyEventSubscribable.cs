using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IRemotePlayerLeaveLobbyEventSubscribable
	{
		event EventHandler<RemotePlayerLeaveLobbyEventArgs> OnRemotePlayerLeftLobby;
	}

	public sealed class RemotePlayerLeaveLobbyEventArgs : EventArgs, IEntityIdentifable
	{
		/// <inheritdoc />
		public int EntityGuid { get; }

		/// <inheritdoc />
		public RemotePlayerLeaveLobbyEventArgs(int entityGuid)
		{
			if(entityGuid < 0) throw new ArgumentOutOfRangeException(nameof(entityGuid));

			EntityGuid = entityGuid;
		}
	}
}
