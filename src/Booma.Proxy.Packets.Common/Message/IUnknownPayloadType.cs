using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IUnknownPayloadType
	{
		/// <summary>
		/// The operation code for the payload.
		/// </summary>
		short OperationCode { get; }

		/// <summary>
		/// The unknown bytes for the payload.
		/// </summary>
		byte[] UnknownBytes { get; }
	}
}
