using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for commands that contain a client id.
	/// </summary>
	public interface ICommandClientIdentifiable
	{
		/// <summary>
		/// The ID associated with the client.
		/// </summary>
		byte ClientId { get; }
	}
}
