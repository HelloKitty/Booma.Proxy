using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IEncryptionServiceProvider
	{
		/// <summary>
		/// Encryptions the provided bytes inplace.
		/// Also returns them.
		/// </summary>
		/// <param name="bytes">The bytes to encrypt.</param>
		/// <returns>The bytes.</returns>
		byte[] Encrypt(byte[] bytes);

		/// <summary>
		/// Decrypts the provided bytes inplace.
		/// Also returns them.
		/// </summary>
		/// <param name="bytes">The bytes to decrypt.</param>
		/// <returns>The bytes.</returns>
		byte[] Decrypt(byte[] bytes);
	}
}
