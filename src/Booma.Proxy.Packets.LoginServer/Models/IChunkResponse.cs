using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// A chunk response.
	/// </summary>
	public interface IChunkResponse
	{
		/// <summary>
		/// The chunk number/id.
		/// </summary>
		uint ChunkNumber { get; }

		/// <summary>
		/// The partial data chunk.
		/// </summary>
		byte[] PartialData { get; }
	}
}
