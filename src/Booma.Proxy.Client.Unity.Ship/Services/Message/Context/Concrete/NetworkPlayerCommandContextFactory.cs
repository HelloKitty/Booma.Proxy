using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{

	/// <summary>
	/// Context factory that can build both
	/// <see cref="INetworkPlayerCommandMessageContext"/> and <see cref="INetworkPlayerFullCommandMessageContext"/>s.
	/// </summary>
	public sealed class NetworkPlayerCommandContextFactory : ICommandMessageContextFactory<ICommandClientIdentifiable, INetworkPlayerFullCommandMessageContext>
	{
		/// <summary>
		/// The network player collection.
		/// </summary>
		private INetworkPlayerCollection PlayerCollection { get; }

		public NetworkPlayerCommandContextFactory(INetworkPlayerCollection playerCollection)
		{
			if(playerCollection == null) throw new ArgumentNullException(nameof(playerCollection));

			PlayerCollection = playerCollection;
		}

		/// <inheritdoc />
		public INetworkPlayerFullCommandMessageContext Create(ICommandClientIdentifiable message)
		{
			//TODO: Create default contexts
			if(!PlayerCollection.ContainsId(message.ClientId))
				return InvalidPlayerCommandMessageContext.Instance;

			//Just create the new context.
			return new NetworkPlayerCommandMessageContext(PlayerCollection[message.ClientId], PlayerCollection.Local);
		}
	}
}
