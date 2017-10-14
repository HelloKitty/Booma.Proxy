using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Data model for a connection endpoint.
	/// </summary>
	public interface IGameConnectionEndpointDetails
	{
		/// <summary>
		/// The port for the connection endpoint.
		/// </summary>
		int Port { get; set; }

		/// <summary>
		/// The IpAddress for the connection endpoint.
		/// </summary>
		string IpAddress { get; set; }
	}
}
