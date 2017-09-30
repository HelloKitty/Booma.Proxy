using System;

namespace Booma.Proxy
{
	public sealed class SeperateAggregateCryptoInitializationService<TVectorType> : IFullCryptoInitializationService<TVectorType>
	{
		/// <inheritdoc />
		public ICryptoKeyInitializable<TVectorType> EncryptionInitializable { get; }

		/// <inheritdoc />
		public ICryptoKeyInitializable<TVectorType> DecryptionInitializable { get; }

		/// <inheritdoc />
		public SeperateAggregateCryptoInitializationService(ICryptoKeyInitializable<TVectorType> encryptionInitializable, ICryptoKeyInitializable<TVectorType> decryptionInitializable)
		{
			if(encryptionInitializable == null) throw new ArgumentNullException(nameof(encryptionInitializable));
			if(decryptionInitializable == null) throw new ArgumentNullException(nameof(decryptionInitializable));

			EncryptionInitializable = encryptionInitializable;
			DecryptionInitializable = decryptionInitializable;
		}
	}
}