using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for types that can have IV/Keys
	/// inserted into them.
	/// </summary>
	public interface ICryptoKeyInitializable<TKeyType>
	{
		/// <summary>
		/// Sets the crypto service with the provided key
		/// </summary>
		/// <param name="key">The key to set.</param>
		void SetKey(TKeyType key);
	}
}
