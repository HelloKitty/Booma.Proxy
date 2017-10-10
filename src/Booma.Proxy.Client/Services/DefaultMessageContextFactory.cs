using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/// <summary>
	/// Default implementation of the <see cref="IClientMessageContextFactory"/>.
	/// </summary>
	public sealed class DefaultMessageContextFactory : IClientMessageContextFactory
	{
		/// <inheritdoc />
		public IClientMessageContext<TPayloadBaseType> Create<TPayloadBaseType>([NotNull] IConnectionService connectionService, [NotNull] IClientPayloadSendService<TPayloadBaseType> sendService, 
			IClientRequestSendService<TPayloadBaseType> requestService)
			where TPayloadBaseType : class
		{
			if(connectionService == null) throw new ArgumentNullException(nameof(connectionService));
			if(sendService == null) throw new ArgumentNullException(nameof(sendService));

			//This doesn't have to work like this, it could be other services/dependencies,
			//but the only implementation right now is the client itself.
			return new DefaultClientMessageContext<TPayloadBaseType>(connectionService, sendService, requestService);
		}
	}
}
