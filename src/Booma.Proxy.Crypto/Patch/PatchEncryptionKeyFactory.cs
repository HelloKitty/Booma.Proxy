using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class PatchEncryptionKeyFactory
	{

		/// <summary>
		/// Creates a new <see cref="PatchEncryptionKey"/> based on the provided
		/// <see cref="vector"/>.
		/// </summary>
		/// <param name="vector">The vector to base the key from.</param>
		/// <returns>A new initalized patch encryption key.</returns>
		public static PatchEncryptionKey Create(uint vector)
		{
			PatchEncryptionKey key = new PatchEncryptionKey();
			InitializeKey(key, vector);

			return key;
		}

		private static void InitializeKey(PatchEncryptionKey key, uint val)
		{
			uint index, tmp;

			tmp = 1;
			key.Keys[55] = val;

			for (int i1 = 0x15; i1 <= 0x46E; i1 += 0x15)
			{
				index = (uint)(i1 % 55);
				val -= tmp;
				key.Keys[index] = tmp;
				tmp = val;
				val = (uint)key.Keys[index];
			}

			for(int i = 0; i < 4; i++)
				key.CRYPT_PC_MixKeys();

			key.Position = 56;
		}
	}
}
