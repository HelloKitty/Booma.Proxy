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
		public PatchEncryptionKey Key { get; }

		public PatchServerCryptoProvider(PatchEncryptionKey key)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));

			Key = key;
		}

		public byte[] Crypt(byte[] bytes)
		{
			CRYPT_PC_CryptData(Key, bytes);
			return bytes;
		}

		public unsafe void CRYPT_PC_CryptData(PatchEncryptionKey key, byte[] data)
		{
			uint x, tmp;

			for(x = 0; x < (uint)data.LongLength; x += 4)
			{
				fixed(void* p = &data[x])
				{
					tmp = *(uint*)p;
				}

				tmp = tmp ^ key.CRYPT_PC_GetNextKey();

				fixed(void* p = &data[x])
				{
					*((uint*)p) = tmp;
				}
			}
		}
	}
}
