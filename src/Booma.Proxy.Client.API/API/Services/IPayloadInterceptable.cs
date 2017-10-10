using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for types that can be short-circuited to allow
	/// payload intercepting.
	/// </summary>
	public interface IPayloadInterceptable
	{
		/// <summary>
		/// Registers an interception request that yields an awaitable for
		/// the specified <typeparamref name="TResponseType"/> type.
		/// </summary>
		/// <typeparam name="TResponseType">The payload type to intercept.</typeparam>
		/// <returns>An awaitable for the next recieved payload of the speified type.</returns>
		Task<TResponseType> InterceptPayload<TResponseType>()
			where TResponseType : IPacketPayload;
	}
}
