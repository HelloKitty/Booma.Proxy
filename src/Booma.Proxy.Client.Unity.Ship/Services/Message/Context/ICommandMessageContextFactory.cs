using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Factory that can create contexts required for
	/// command handling.
	/// </summary>
	/// <typeparam name="TMessageType">The Type of the message.</typeparam>
	/// <typeparam name="TContextType"></typeparam>
	public interface ICommandMessageContextFactory<in TMessageType, out TContextType>
		where TContextType : ICommandMessageContext
	{
		/// <summary>
		/// Creates a new <typeparamref name="TMessageType"/>
		/// </summary>
		/// <param name="message">The context message to be handled.</param>
		/// <returns>Returns a new non-null context. May be invalid, check <see cref="ICommandMessageContext"/>'s model state.</returns>
		TContextType Create(TMessageType message);
	}
}
