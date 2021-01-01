using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	public interface IUnknownPayloadType
	{
		/// <summary>
		/// The unknown bytes for the payload.
		/// </summary>
		byte[] UnknownBytes { get; }
	}
}
