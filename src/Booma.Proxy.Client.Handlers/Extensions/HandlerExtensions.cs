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

		/// <summary>
		/// Deocrates the payload handler in a generalized message handler with try semantics.
		/// This handler will try to consume messages and if not will indicate that the message wasn't consumed.
		/// This allows callers to try to handle a message that they don't know the payload type of and look for another consumer
		/// if it fails.
		/// </summary>
		/// <typeparam name="TPayloadType">The payload type (likely inferred)</typeparam>
		/// <typeparam name="TIncomingPayloadType"></typeparam>
		/// <typeparam name="TOutgoingPayloadType"></typeparam>
		/// <param name="handler">The non-null handler to decorate.</param>
		/// <returns>A new generalized message handler with try semantics.</returns>
		public static IClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType> AsTryHandler<TPayloadType, TIncomingPayloadType, TOutgoingPayloadType>(this IClientPayloadSpecificMessageHandler<TPayloadType, TOutgoingPayloadType> handler)
			where TPayloadType : class, TIncomingPayloadType
			where TOutgoingPayloadType : class
			where TIncomingPayloadType : class
		{
			if(handler == null) throw new ArgumentNullException(nameof(handler));

			//We decorate the handler in try semantics
			return new TrySemanticsBasedOnTypeClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType, TPayloadType>(handler);
		}

		/// <summary>
		/// Decorates the handler with payload interception functionality.
		/// </summary>
		/// <typeparam name="TIncomingPayloadType"></typeparam>
		/// <typeparam name="TOutgoingPayloadType"></typeparam>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static InterceptAsyncRequestClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType> WithInterception<TIncomingPayloadType, TOutgoingPayloadType>(this IClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType> handler) 
			where TIncomingPayloadType : class 
			where TOutgoingPayloadType : class
		{
			return new InterceptAsyncRequestClientMessageHandler<TIncomingPayloadType, TOutgoingPayloadType>(handler);
		}
	}
}
