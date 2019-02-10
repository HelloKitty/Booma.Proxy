using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IClientFinishedBurstingEventSubscribable
	{
		event EventHandler<ClientBurstingEndingEventArgs> OnClientBurstingFinished;
	}

	public sealed class ClientBurstingEndingEventArgs : EventArgs
	{
		public int EntityGuid { get; }

		/// <summary>
		/// It's possible that the burst ending was a failure.
		/// So consider that when checking/listening to this event.
		/// </summary>
		public bool isSuccessful { get; }

		/// <inheritdoc />
		public ClientBurstingEndingEventArgs(int entityGuid, bool isSuccessful)
		{
			//TODO: Verify entity guid.

			EntityGuid = entityGuid;
			this.isSuccessful = isSuccessful;
		}
	}
}
