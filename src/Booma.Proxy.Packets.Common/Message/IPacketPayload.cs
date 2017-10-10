using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Marker interface for payload types.
	/// </summary>
	public interface IPacketPayload
	{
		//Nothing, just lets us try to more safely constrain generics
		//where we don't have access to the actual payload type
	}
}
