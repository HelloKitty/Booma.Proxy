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
			ulong esi, ebx, edi, eax, edx, var1;
			esi = 1;
			ebx = val;
			edi = 0x15;
			key.Keys[56] = ebx;
			key.Keys[55] = ebx;

			while(edi <= 0x46E)
			{
				eax = edi;
				var1 = eax / 55;
				edx = eax - (var1 * 55);
				ebx = ebx - esi;
				edi = edi + 0x15;
				key.Keys[edx] = esi;
				esi = ebx;
				ebx = key.Keys[edx];
			}

			for(int i = 0; i < 4; i++)
				key.CRYPT_PC_MixKeys();

			key.Position = 56;
		}
	}
}
