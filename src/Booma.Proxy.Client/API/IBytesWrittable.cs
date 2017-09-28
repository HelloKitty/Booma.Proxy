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
		/// Writes the provided <see cref="bytes"/> starting at the <see cref="offset"/>
		/// for <see cref="count"/> many bytes.
		/// </summary>
		/// <param name="bytes">The bytes to write.</param>
		/// <param name="offset">The offset to start at.</param>
		/// <param name="count">The amount of bytes to write.</param>
		void Write(byte[] bytes, int offset, int count);

		/// <summary>
		/// Writes the provided <see cref="bytes"/> asyncronously.
		/// </summary>
		/// <param name="bytes">The bytes to write.</param>
		/// <returns>An awaitable task.</returns>
		Task WriteAsync(byte[] bytes);

		/// <summary>
		/// Asyncronously the provided <see cref="bytes"/> starting at the <see cref="offset"/>
		/// for <see cref="count"/> many bytes.
		/// </summary>
		/// <param name="bytes">The bytes to write.</param>
		/// <param name="offset">The offset to start at.</param>
		/// <param name="count">The amount of bytes to write.</param>
		Task WriteAsync(byte[] bytes, int offset, int count);
	}
}
