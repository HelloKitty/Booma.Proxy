using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
    /// <summary>
    /// Blowfish cipher library modified for PSOBB
    /// </summary>
    public class BlowfishCipher
    {
        /// <summary>
        /// Internal P storage
        /// </summary>
        private uint[] _p;
        /// <summary>
        /// Internal S storage
        /// </summary>
        private uint[][] _s;

        /// <summary>
        /// XOR key before calling Cipher.Init, PSOBB does it but since it's
        /// not part of the Init procedure (not even on BB), it goes separated 
        /// and the user is in charge of xoring the key before being used.
        /// </summary>
        /// <param name="key"></param>
        public void PSOBBInitKey(byte[] key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length != 48)
            {
                throw new ArgumentException("Invalid key length, key length must be 48");
            }

            // The theoretical max length for the key (56) would not fit for a full xor

            for (int i1 = 0; i1 < 48; i1 += 3)
            {
                key[i1] ^= 0x19;
                key[i1 + 1] ^= 0x16;
                key[i1 + 2] ^= 0x18;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="standard"></param>
        public BlowfishCipher()
        {
            this._p = new uint[18];
            this._s = new uint[4][]
            {
                new uint[256],
                new uint[256],
                new uint[256],
                new uint[256],
            };
        }

        /// <summary>
        /// Table-less cipher initialization.
        /// In normal operation, use this.
        /// </summary>
        /// <param name="key"></param>
        public void Init(byte[] key)
        {
            this.Init(BlowfishTable.p, BlowfishTable.s, key);
        }

        /// <summary>
        /// Cipher initialization
        /// </summary>
        /// <param name="p"></param>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <remarks>
        /// Ideally, tables would be loaded internally, but this way
        /// external tables can be loaded for testing purposes<para />
        /// </remarks>
        public void Init(uint[] p, uint[][] s, byte[] key)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }
            if (p.Length != 18)
            {
                throw new ArgumentException("Invalid p length, p length must be 18");
            }
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (s.Length != 4)
            {
                throw new ArgumentException("Invalid s length, s length must be 4");
            }
            for (int i1 = 0; i1 < 4; i1++)
            {
                if (s[i1] == null)
                {
                    throw new ArgumentNullException($"s[{i1}]");
                }
                if (s[i1].Length != 256)
                {
                    throw new ArgumentException($"Invalid s[{i1}] length, s[{i1}] length must be 256");
                }
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length == 0 || key.Length >= 56)
            {
                throw new ArgumentException("Invalid key length, key length must be higher than 0 and less than 56");
            }

            Array.Copy(p, this._p, 18);
            Array.Copy(s[0], this._s[0], 256);
            Array.Copy(s[1], this._s[1], 256);
            Array.Copy(s[2], this._s[2], 256);
            Array.Copy(s[3], this._s[3], 256);

            // This xoring is not part of the standard blowfish.
            for (int i1 = 0; i1 < 18; i1++)
            {
                ushort pT = (ushort)this._p[i1];
                pT = (ushort)(((pT & 0x00FF) << 8) + ((pT & 0xFF00) >> 8));
                this._p[i1] = (((p[i1] >> 16) ^ pT) << 16) + pT;
            }

            for (int i1 = 0; i1 < 18; i1++)
            {
                uint k = (uint)
                    ((key[(i1 * 4 + 3) % key.Length]) +
                    ((key[(i1 * 4 + 2) % key.Length]) << 8) +
                    ((key[(i1 * 4 + 1) % key.Length]) << 16) +
                    ((key[(i1 * 4 + 0) % key.Length]) << 24));
                this._p[i1] ^= k;
            }

            uint v1 = 0, v2 = 0;
            for (int i1 = 0; i1 < 18; i1 += 2)
            {
                InitBlock(ref v1, ref v2);
                this._p[i1] = v1;
                this._p[i1 + 1] = v2;
            }
            for (int i1 = 0; i1 < 4; i1++)
            {
                for (int i2 = 0; i2 < 256; i2 += 2)
                {
                    InitBlock(ref v1, ref v2);
                    this._s[i1][i2] = v1;
                    this._s[i1][i2 + 1] = v2;
                }
            }
        }
        private void InitBlock(ref uint L, ref uint R)
        {
            for (int i1 = 0; i1 < 16; i1 += 2)
            {
                L ^= this._p[i1];
                R ^= F(L);
                R ^= this._p[i1 + 1];
                L ^= F(R);
            }
            L ^= this._p[16 + 0];
            R ^= this._p[16 + 1];

            uint t = L;
            L = R;
            R = t;
        }

        /// <summary>
        /// Encrypt data
        /// </summary>
        /// <param name="data">Data to encrypt</param>
        /// <param name="offset">Position where the encryption will start</param>
        /// <param name="length">Number of bytes to encrypt</param>
        public void Encrypt(byte[] data, int offset, int length)
        {
            uint v1, v2;
            for (int i1 = 0; i1 < (length / 8); i1++)
            {
                v1 = UInt32(data, offset + i1 * 8);
                v2 = UInt32(data, offset + i1 * 8 + 4);
                EncryptBlock(ref v1, ref v2);
                UInt32(data, offset + i1 * 8, v1);
                UInt32(data, offset + i1 * 8 + 4, v2);
            }
        }
        private void EncryptBlock(ref uint L, ref uint R)
        {
            for (int i1 = 0; i1 < 4; i1 += 2)
            {
                L ^= this._p[i1];
                R ^= F(L);
                R ^= this._p[i1 + 1];
                L ^= F(R);
            }
            L ^= this._p[4 + 0];
            R ^= this._p[4 + 1];

            uint t = L;
            L = R;
            R = t;
        }

        /// <summary>
        /// Decrypt data
        /// </summary>
        /// <param name="data">Data to decrypt</param>
        /// <param name="offset">Position where the decryption will start</param>
        /// <param name="length">Number of bytes to decrypt</param>
        public void Decrypt(byte[] data, int offset, int length)
        {
            uint v1, v2;
            for (int i1 = 0; i1 < (length / 8); i1++)
            {
                v1 = UInt32(data, offset + i1 * 8);
                v2 = UInt32(data, offset + i1 * 8 + 4);
                DecryptBlock(ref v1, ref v2);
                UInt32(data, offset + i1 * 8, v1);
                UInt32(data, offset + i1 * 8 + 4, v2);
            }
        }
        private void DecryptBlock(ref uint L, ref uint R)
        {
            for (int i1 = 4; i1 > 0; i1 -= 2)
            {
                L ^= this._p[i1 + 1];
                R ^= F(L);
                R ^= this._p[i1];
                L ^= F(R);
            }
            L ^= this._p[1];
            R ^= this._p[0];

            uint t = L;
            L = R;
            R = t;
        }

        /// <summary>
        /// Blowfish F
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private uint F(uint x)
        {
            uint e;
            e = this._s[0][(byte)(x >> 24)];
            e += this._s[1][(byte)(x >> 16)];
            e ^= this._s[2][(byte)(x >> 8)];
            e += this._s[3][(byte)(x)];
            return e;
        }

        /// <summary>
        /// Get uint bytes from byte array
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private uint UInt32(byte[] data, int offset)
        {
            return (uint)(data[offset + 0] + (data[offset + 1] << 8) + (data[offset + 2] << 16) + (data[offset + 3] << 24));
        }
        /// <summary>
        /// Put uint bytes into byte array
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        private void UInt32(byte[] data, int offset, uint value)
        {
            data[offset + 0] = (byte)(value);
            data[offset + 1] = (byte)(value >> 8);
            data[offset + 2] = (byte)(value >> 16);
            data[offset + 3] = (byte)(value >> 24);
        }
    }
}
