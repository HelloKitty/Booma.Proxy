using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// A pair of crypto initializers.
	/// </summary>
	/// <typeparam name="TVectorType">The type of vector input.</typeparam>
	public interface IFullCryptoInitializationService<TVectorType>
	{
		/// <summary>
		/// The encryption initializable.
		/// </summary>
		ICryptoKeyInitializable<TVectorType> EncryptionInitializable { get; }

		/// <summary>
		/// The decryption initializable.
		/// </summary>
		ICryptoKeyInitializable<TVectorType> DecryptionInitializable { get; }
	}
}
