using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	//Based on: https://github.com/Sylverant/libsylverant/blob/e1a01d5586ed12d41b99c5cf1ba955e32b173950/include/sylverant/encryption.h#L28
	public class PatchEncryptionKey
	{
		// Encryption data struct 
		public uint[] Keys { get; set; }

		public uint Position { get; set; }

		internal PatchEncryptionKey()
		{
			Keys = new uint[1024];
		}

		public void CRYPT_PC_MixKeys()
		{
			for (int i1 = 0x18, i2 = 0x01; i1 > 0; i1--, i2++)
			{
				Keys[i2] -= Keys[i2 + 0x1F];
			}
			for (int i1 = 0x1F, i2 = 0x19; i1 > 0; i1--, i2++)
			{
				Keys[i2] -= Keys[i2 - 0x18];
			}
		}

		public uint CRYPT_PC_GetNextKey()
		{
			uint re;

			if(Position == 56)
			{
				CRYPT_PC_MixKeys();
				Position = 1;
			}

			re = Keys[Position];
			Position++;

			return re;
		}
	}
}
