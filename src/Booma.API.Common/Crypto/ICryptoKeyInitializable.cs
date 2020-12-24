using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Contract for types that can have IV/Keys
	/// inserted into them.
	/// </summary>
	public interface ICryptoKeyInitializable<in TKeyType>
	{
		/// <summary>
		/// Sets the crypto service with the provided key
		/// </summary>
		/// <param name="key">The key to set.</param>
		void Initialize(TKeyType key);

		/// <summary>
		/// Uninitializes the crypto service.
		/// It forgets the current key and will stop passing
		/// to crypt.
		/// </summary>
		void Uninitialize();
	}
}