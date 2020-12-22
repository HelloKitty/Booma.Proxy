using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	public interface ICryptoServiceProvider
	{
		/// <summary>
		/// Indicates the size of the blocks that
		/// must be a multiple of for crypting.
		/// If 1 or 0 this means that no block size
		/// is required.
		/// </summary>
		int BlockSize { get; }

		/// <summary>
		/// Passes the bytes inplace through the cipher.
		/// Also returns them.
		/// </summary>
		/// <param name="bytes">The bytes to crypt.</param>
		/// <returns>The bytes.</returns>
		byte[] Crypt(byte[] bytes);

		/// <summary>
		/// Passes the bytes inplace through the cipher starting at the 
		/// specified offset and ended after count many bytes have been ciphered.
		/// Also returns them.
		/// </summary>
		/// <param name="bytes">The bytes to crypt.</param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		/// <returns>The bytes.</returns>
		byte[] Crypt(byte[] bytes, int offset, int count);
	}
}