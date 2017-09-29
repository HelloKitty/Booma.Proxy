using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
		PSOBBNetworkIncomingMessage<TPayloadBaseType> Read();

		/// <summary>
		/// Reads an incoming message asyncronously.
		/// The task will complete when an incomding message can be built.
		/// </summary>
		/// <returns>A future for the resulting incoming message.</returns>
		Task<PSOBBNetworkIncomingMessage<TPayloadBaseType>> ReadAsync();

		/// <summary>
		/// Reads an incoming message asyncronously.
		/// The task will complete when an incomding message can be built.
		/// </summary>
		/// <param name="token">The token that can cancel the operation.</param>
		/// <returns>A future for the resulting incoming message.</returns>
		Task<PSOBBNetworkIncomingMessage<TPayloadBaseType>> ReadAsync(CancellationToken token);
	}
}
