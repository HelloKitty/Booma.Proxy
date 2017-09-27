using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IBytesWrittable
	{
		/// <summary>
		/// Writes the provided <see cref="bytes"/>.
		/// </summary>
		/// <param name="bytes">The bytes to write.</param>
		void Write(byte[] bytes);

		/// <summary>
		/// Writes the provided <see cref="bytes"/> asyncronously.
		/// </summary>
		/// <param name="bytes">The bytes to write.</param>
		/// <returns>An awaitable task.</returns>
		Task WriteAsync(byte[] bytes);
	}
}
