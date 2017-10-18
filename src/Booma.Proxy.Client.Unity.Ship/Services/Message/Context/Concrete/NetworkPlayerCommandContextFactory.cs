using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{

	/// <summary>
	/// Context factory that can build both
	/// <see cref="INetworkPlayerNetworkMessageContext"/> and <see cref="INetworkPlayerFullNetworkMessageContext"/>s.
	/// </summary>
	public sealed class NetworkPlayerCommandContextFactory : INetworkMessageContextFactory<IMessageContextIdentifiable, INetworkPlayerFullNetworkMessageContext>
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
		public INetworkPlayerFullNetworkMessageContext Create(IMessageContextIdentifiable message)
		{
			//TODO: Create default contexts
			if(!PlayerCollection.ContainsId(message.Identifier))
				return InvalidPlayerCommandMessageContext.Instance;

			//Just create the new context.
			return new NetworkPlayerCommandMessageContext(PlayerCollection[message.Identifier], PlayerCollection.Local);
		}
	}
}
