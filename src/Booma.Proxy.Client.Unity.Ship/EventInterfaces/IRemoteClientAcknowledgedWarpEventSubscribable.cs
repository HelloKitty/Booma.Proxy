using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IRemoteClientAcknowledgedWarpEventSubscribable
	{
		event EventHandler<RemotePlayerWarpAcknowledgementEventArgs> OnRemotePlayerAcknowledgedWarp;
	}

	public sealed class RemotePlayerWarpAcknowledgementEventArgs : EventArgs, IEntityIdentifable
	{
		/// <inheritdoc />
		public int EntityGuid { get; }

		/// <inheritdoc />
		public RemotePlayerWarpAcknowledgementEventArgs(int entityGuid)
		{
			//TODO: Verify guid.

			EntityGuid = entityGuid;
		}
	}
}
