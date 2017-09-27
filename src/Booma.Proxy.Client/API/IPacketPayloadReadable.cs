using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for types that can write a packet payload.
	/// </summary>
	public interface IPacketPayloadReadable<TPayloadBaseType>
		where TPayloadBaseType : class
	{
		/// <summary>
		/// Reads an incoming message syncronously and blocks until it recieves one.
		/// </summary>
		/// <returns>The resulting incoming message.</returns>
		PSOBBNetworkIncomdingMessage<TPayloadBaseType> Read();

		/// <summary>
		/// Reads an incoming message asyncronously.
		/// The task will complete when an incomding message can be built.
		/// </summary>
		/// <returns>A future for the resulting incoming message.</returns>
		Task<PSOBBNetworkIncomdingMessage<TPayloadBaseType>> ReadAsync();
	}
}
