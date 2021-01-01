using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Model for a chunk request.
	/// </summary>
	public interface IChunkRequest
	{
		/// <summary>
		/// The chunk number to request.
		/// </summary>
		uint ChunkNumber { get; }
	}
}
