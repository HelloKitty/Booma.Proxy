using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for a service that is able to send payloads like <see cref="IClientPayloadSendService{TPayloadBaseType}"/>
	/// but can also return a future that will complete when a payload of that type is recieved.
	/// Implementers should make sure to shortcircuit to prevent multiple of the same payload being handled.
	/// </summary>
	/// <typeparam name="TPayloadBaseType"></typeparam>
	public interface IClientRequestSendService<in TPayloadBaseType>
		where TPayloadBaseType : class
	{
		/// <summary>
		/// Sends the <see cref="request"/> payload and provided a future awaitable
		/// that can yield the <typeparamref name="TResponseType"/> payload.
		/// </summary>
		/// <typeparam name="TResponseType"></typeparam>
		/// <param name="request">The request payload.</param>
		/// <returns></returns>
		Task<TResponseType> SendRequestAsync<TResponseType>(TPayloadBaseType request)
			where TResponseType : IPacketPayload;
	}
}
