using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	public sealed class ProxiedFullCryptoInitializable : IFullCryptoInitializationService<byte[]>
	{
		/// <inheritdoc />
		public GladNet.ICryptoKeyInitializable<byte[]> EncryptionInitializable { get; }

		/// <inheritdoc />
		public GladNet.ICryptoKeyInitializable<byte[]> DecryptionInitializable { get; }

		/// <inheritdoc />
		public ProxiedFullCryptoInitializable([NotNull] GladNet.ICryptoKeyInitializable<byte[]> encryptionInitializable, [NotNull] GladNet.ICryptoKeyInitializable<byte[]> decryptionInitializable)
		{
			EncryptionInitializable = encryptionInitializable ?? throw new ArgumentNullException(nameof(encryptionInitializable));
			DecryptionInitializable = decryptionInitializable ?? throw new ArgumentNullException(nameof(decryptionInitializable));
		}
	}
}
