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
		public ulong[] Keys { get; set; }

		public ulong Position { get; set; }

		internal PatchEncryptionKey()
		{
			Keys = new ulong[1024];
		}

		public void CRYPT_PC_MixKeys()
		{
			ulong esi, edi, eax, ebp, edx;
			edi = 1;
			edx = 0x18;
			eax = edi;
			while(edx > 0)
			{
				esi = Keys[eax + 0x1F];
				ebp = Keys[eax];
				ebp = ebp - esi;
				Keys[eax] = ebp;
				eax++;
				edx--;
			}
			edi = 0x19;
			edx = 0x1F;
			eax = edi;
			while(edx > 0)
			{
				esi = Keys[eax - 0x18];
				ebp = Keys[eax];
				ebp = ebp - esi;
				Keys[eax] = ebp;
				eax++;
				edx--;
			}
		}

		public ulong CRYPT_PC_GetNextKey()
		{
			ulong re;

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
