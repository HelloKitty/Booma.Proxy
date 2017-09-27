using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for client that exposed <see cref="IPacketHeader"/> reading
	/// capabilities.
	/// </summary>
	public interface IPacketHeaderReadable : IConnectable, IDisconnectable, IDisposable,
		IBytesWrittable, IBytesReadable
	{
		/// <summary>
		/// Attempts to read a <see cref="IPacketHeader"/> from
		/// the client.
		/// </summary>
		/// <returns>A PSOBBPacketHeader.</returns>
		IPacketHeader ReadHeader();

		/// <summary>
		/// Attempts to read a <see cref="IPacketHeader"/> from
		/// the client.
		/// </summary>
		/// <returns>A PSOBBPacketHeader.</returns>
		Task<IPacketHeader> ReadHeaderAsync();
	}
}
