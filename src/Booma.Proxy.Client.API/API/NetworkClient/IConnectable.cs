using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IConnectable
	{
		/// <summary>
		/// Connects to the provided <see cref="ip"/> with on the given <see cref="port"/>.
		/// </summary>
		/// <param name="ip">The ip.</param>
		/// <param name="port">The port.</param>
		/// <returns>True if connection was successful.</returns>
		bool Connect(string ip, int port);

		/// <summary>
		/// Connects to the provided <see cref="address"/> with on the given <see cref="port"/>.
		/// </summary>
		/// <param name="address">The ip.</param>
		/// <param name="port">The port.</param>
		/// <returns>True if connection was successful.</returns>
		bool Connect(IPAddress address, int port);

		/// <summary>
		/// Connects to the provided <see cref="ip"/> with on the given <see cref="port"/>.
		/// </summary>
		/// <param name="ip">The ip.</param>
		/// <param name="port">The port.</param>
		/// <returns>True if connection was successful.</returns>
		Task<bool> ConnectAsync(string ip, int port);

		/// <summary>
		/// Connects to the provided <see cref="address"/> with on the given <see cref="port"/>.
		/// </summary>
		/// <param name="address">The ip.</param>
		/// <param name="port">The port.</param>
		/// <returns>True if connection was successful.</returns>
		Task<bool> ConnectAsync(IPAddress address, int port);
	}
}
