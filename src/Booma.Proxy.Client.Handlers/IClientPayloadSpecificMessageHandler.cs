using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for handlers that handle a specific derived type payload <typeparamref name="TPayloadType"/>
	/// that derives from <see cref="TPayloadBaseType"/>.
	/// </summary>
	/// <typeparam name="TPayloadType">The type of payload that is handled.</typeparam>
	/// <typeparam name="TPayloadBaseType">The base type of the payload.</typeparam>
	public interface IClientPayloadSpecificMessageHandler<TPayloadType, TPayloadBaseType>
		where TPayloadBaseType : class
		where TPayloadType : class, TPayloadBaseType
	{
		/// <summary>
		/// Handles the message with <see cref="context"/> provided and correctly typed
		/// <see cref="payload"/>.
		/// </summary>
		/// <param name="context">The message context.</param>
		/// <param name="payload">The payload to handle.</param>
		Task HandleMessage(IClientMessageContext<TPayloadBaseType> context, TPayloadType payload);
	}
}
