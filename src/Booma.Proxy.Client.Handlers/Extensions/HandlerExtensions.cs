using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Extension methods for handler types.
	/// </summary>
	public static class HandlerExtensions
	{

		/// <summary>
		/// Deocrates the payload handler in a generalized message handler with try semantics.
		/// This handler will try to consume messages and if not will indicate that the message wasn't consumed.
		/// This allows callers to try to handle a message that they don't know the payload type of and look for another consumer
		/// if it fails.
		/// </summary>
		/// <typeparam name="TPayloadType">The payload type (likely inferred)</typeparam>
		/// <param name="handler">The non-null handler to decorate.</param>
		/// <returns>A new generalized message handler with try semantics.</returns>
		public static IClientMessageHandler<PSOBBPatchPacketPayloadServer, PSOBBPatchPacketPayloadClient> AsTryHandler<TPayloadType>(this IClientPayloadSpecificMessageHandler<TPayloadType, PSOBBPatchPacketPayloadClient> handler)
			where TPayloadType : PSOBBPatchPacketPayloadServer
		{
			if(handler == null) throw new ArgumentNullException(nameof(handler));

			//We decorate the handler in try semantics
			return new TrySemanticsBasedOnTypeClientMessageHandler<PSOBBPatchPacketPayloadServer,PSOBBPatchPacketPayloadClient,TPayloadType>(handler);
		}
	}
}
