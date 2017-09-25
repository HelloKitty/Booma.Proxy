using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class PatchServerCryptoProvider : IEncryptionServiceProvider
	{
		/// <summary>
		/// The encryption key for encryption.
		/// </summary>
		public PatchEncryptionKey EncryptionKey { get; }

		/// <summary>
		/// The decryption key for decryption.
		/// </summary>
		public PatchEncryptionKey DecryptionKey { get; }

		public PatchServerCryptoProvider(PatchEncryptionKey encryptionKey, PatchEncryptionKey decryptionKey)
		{
			if(encryptionKey == null) throw new ArgumentNullException(nameof(encryptionKey));
			if(decryptionKey == null) throw new ArgumentNullException(nameof(decryptionKey));

			EncryptionKey = encryptionKey;
			DecryptionKey = decryptionKey;
		}

		public byte[] Encrypt(byte[] bytes)
		{
			CRYPT_PC_CryptData(EncryptionKey, bytes);
			return bytes;
		}

		public byte[] Decrypt(byte[] bytes)
		{
			CRYPT_PC_CryptData(DecryptionKey, bytes);
			return bytes;
		}

		public unsafe void CRYPT_PC_CryptData(PatchEncryptionKey key, byte[] data)
		{
			ulong x, tmp;

			for(x = 0; x < (ulong)data.LongLength; x += 4)
			{
				fixed(void* p = &data[x])
				{
					tmp = *(ulong*)p;
				}

				tmp = tmp ^ key.CRYPT_PC_GetNextKey();

				fixed(void* p = &data[x])
				{
					*((ulong*)p) = tmp;
				}
			}
		}
	}
}
