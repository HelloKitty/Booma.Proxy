using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class PatchServerCryptoProvider : ICryptoServiceProvider
	{
		//The patching crypto only requires a 4 blocksize
		/// <inheritdoc />
		public int BlockSize { get; } = 4;

		/// <summary>
		/// The encryption key for encryption.
		/// </summary>
		public PatchEncryptionKey Key { get; }

		public PatchServerCryptoProvider(PatchEncryptionKey key)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));

			Key = key;
		}

		//TODO: Gett fixed pointer to span may be unsafe, we need pinnable reference probably.
		public unsafe void CRYPT_PC_CryptData(PatchEncryptionKey key, Span<byte> data, int offset, int count)
		{
			for(uint x = (uint)offset; x < count; x += 4)
			{
				fixed(void* p = &data[(int)x])
				{
					*(uint*)p = (*(uint*)p) ^ key.CRYPT_PC_GetNextKey();
				}
			}
		}

		public void Crypt(Span<byte> bytes, int offset, int count)
		{
			if(count % 4 != 0)
				throw new InvalidOperationException($"{GetType().Name} cannot crypt N % 4 != 0 bytes. Bytes or count must be a multiple of 4.");

			if(offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
			if(count < 0) throw new ArgumentOutOfRangeException(nameof(count));

			CRYPT_PC_CryptData(Key, bytes, offset, count);
		}
	}
}
