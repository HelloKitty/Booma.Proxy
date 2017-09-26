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
			for(uint x = 0; x < data.Length; x += 4)
			{
				fixed(void* p = &data[x])
				{
					*(uint*)p = (*(uint*)p) ^ key.CRYPT_PC_GetNextKey();
				}
			}
		}
	}
}
