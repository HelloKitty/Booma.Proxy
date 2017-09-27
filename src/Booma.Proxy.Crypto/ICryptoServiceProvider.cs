using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface ICryptoServiceProvider
	{
		/// <summary>
		/// Passes the bytes inplace through the cipher.
		/// Also returns them.
		/// </summary>
		/// <param name="bytes">The bytes to crypt.</param>
		/// <returns>The bytes.</returns>
		byte[] Crypt(byte[] bytes);
	}
}
