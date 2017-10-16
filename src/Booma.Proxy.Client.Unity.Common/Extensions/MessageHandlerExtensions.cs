using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public static class MessageHandlerExtensions
	{
		/// <summary>
		/// Gets the loggable message name that the handler handles.
		/// </summary>
		/// <typeparam name="TSubMessageType">The message type.</typeparam>
		/// <typeparam name="TPayloadType">The payload type.</typeparam>
		/// <param name="handler">The message handler.</param>
		/// <returns>The string name of the message type.</returns>
		public static string MessageName<TSubMessageType, TPayloadType>(this SubMessageMessageHandler<TSubMessageType, TPayloadType> handler) 
			where TPayloadType : PSOBBGamePacketPayloadServer
		{
			if(handler == null) throw new ArgumentNullException(nameof(handler));

			return typeof(TSubMessageType).Name;
		}
	}
}
