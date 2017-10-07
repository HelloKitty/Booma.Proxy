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
	/// <typeparam name="TOutgoingPayloadType"></typeparam>
	public interface IClientPayloadSpecificMessageHandler<in TPayloadType, out TOutgoingPayloadType>
		where TOutgoingPayloadType : class
		where TPayloadType : class
	{
		/// <summary>
		/// Handles the message with <see cref="context"/> provided and correctly typed
		/// <see cref="payload"/>.
		/// </summary>
		/// <param name="context">The message context.</param>
		/// <param name="payload">The payload to handle.</param>
		Task HandleMessage(IClientMessageContext<TOutgoingPayloadType> context, TPayloadType payload);
	}
}
