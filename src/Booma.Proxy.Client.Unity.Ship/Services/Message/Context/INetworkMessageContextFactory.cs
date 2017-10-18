using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Factory that can create contexts required for
	/// message handling.
	/// </summary>
	/// <typeparam name="TMessageType">The Type of the message.</typeparam>
	/// <typeparam name="TContextType">The type of context this factory produces.</typeparam>
	public interface INetworkMessageContextFactory<in TMessageType, out TContextType>
		where TContextType : INetworkMessageContext
	{
		/// <summary>
		/// Creates a new <typeparamref name="TMessageType"/>
		/// </summary>
		/// <param name="message">The context message to be handled.</param>
		/// <returns>Returns a new non-null context. May be invalid, check <see cref="INetworkMessageContext"/>'s model state.</returns>
		TContextType Create(TMessageType message);
	}
}
