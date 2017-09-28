using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IBytesReadable
	{
		/// <summary>
		/// Reads <see cref="count"/> many bytes from the reader.
		/// </summary>
		/// <param name="count">How many bytes to read.</param>
		/// <returns>The read bytes.</returns>
		byte[] Read(int count);

		/// <summary>
		/// Reads asyncronously <see cref="count"/> many bytes from the reader.
		/// </summary>
		/// <param name="count">How many bytes to read.</param>
		/// <param name="timeoutInMilliseconds">How many milliseconds to wait before canceling the operation.</param>
		/// <returns>A future for the read bytes.</returns>
		Task<byte[]> ReadAsync(int count, int timeoutInMilliseconds);

		/// <summary>
		/// Reads asyncronously <see cref="count"/> many bytes from the reader.
		/// </summary>
		/// <param name="buffer">The buffer to store the bytes into.</param>
		/// <param name="start">The start position in the buffer to start reading into.</param>
		/// <param name="count">How many bytes to read.</param>
		/// <param name="timeoutInMilliseconds">How many milliseconds to wait before canceling the operation.</param>
		/// <returns>A future for the read bytes.</returns>
		Task<byte[]> ReadAsync(byte[] buffer, int start, int count, int timeoutInMilliseconds);
	}
}
