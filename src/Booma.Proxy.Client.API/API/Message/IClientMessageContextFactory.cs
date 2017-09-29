using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for types that provide construction functionality
	/// for <see cref="IClientMessageContext{TPayloadBaseType}"/>.
	/// </summary>
	public interface IClientMessageContextFactory
	{
		//TODO: Update doc
		/// <summary>
		/// Creates a new client message context based around the <see cref="client"/>
		/// and contextless relative to the actual message.
		/// </summary>
		/// <typeparam name="TPayloadBaseType">The payload basetype.</typeparam>
		/// <returns>A new message context.</returns>
		IClientMessageContext<TPayloadBaseType> Create<TPayloadBaseType>(IConnectionService connectionService, IClientPayloadSendService<TPayloadBaseType> sendService)
			where TPayloadBaseType : class;
	}
}
