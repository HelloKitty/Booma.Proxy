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
		/// Creates a new <see cref="IClientMessageHandler{TPayloadBaseType}"/> around the provided <see cref="handler"/>.
		/// This greatly simplifies the Type, interactions with it and reduces the generic type arguments of the type without
		/// reducing its usability. Provides a way to check if a particular message can be handled before dispatching it.
		/// </summary>
		/// <typeparam name="TPayloadType">The payload type.</typeparam>
		/// <typeparam name="TPayloadBaseType">The base payload type.</typeparam>
		/// <param name="handler">The handler to wrap.</param>
		/// <returns>A new client message handler with Try semantics.</returns>
		public static IClientMessageHandler<TPayloadBaseType> AsTryMessageHandler<TPayloadType, TPayloadBaseType>(this IClientPayloadSpecificMessageHandler<TPayloadType, TPayloadBaseType> handler)
			where TPayloadBaseType : class
			where TPayloadType : class, TPayloadBaseType
		{
			if(handler == null) throw new ArgumentNullException(nameof(handler));

			//Just decorate the handler in a simplier more generic handler that indicates
			//if it can be handled or not.
			return new TrySemanticsBasedOnTypeClientMessageHandler<TPayloadType, TPayloadBaseType>(handler);
		}
	}
}
