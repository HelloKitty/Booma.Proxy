using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for types that provide construction functionality
	/// for <see cref="IClientMessageContext{TPayloadBaseType}"/>.
	/// </summary>
	public interface IClientMessageContextFactory
	{
		/// <summary>
		/// Creates a new client message context based around the <see cref="client"/>
		/// and contextless relative to the actual message.
		/// </summary>
		/// <typeparam name="TClientType">The client type.</typeparam>
		/// <typeparam name="TPayloadBaseType">The payload basetype.</typeparam>
		/// <param name="client">The client to use as the base for the context/</param>
		/// <returns>A new message context.</returns>
		IClientMessageContext<TPayloadBaseType> Create<TClientType, TPayloadBaseType>(TClientType client)
			where TPayloadBaseType : class
			where TClientType : IConnectable, IDisconnectable, IPacketPayloadWritable<TPayloadBaseType>;
	}
}
