using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public abstract class BaseBlowfishCryptoService : ICryptoServiceProvider, ICryptoKeyInitializable<byte[]>
	{
		/// <summary>
		/// Internal P storage
		/// </summary>
		private uint[] _p;

		protected uint[] P => _p;

		/// <summary>
		/// Internal S storage
		/// </summary>
		private uint[][] _s;

		protected uint[][] S => _s;

		/// <summary>
		/// Indicates if the cryption service has been init'd.
		/// </summary>
		protected bool isInitialized { get; private set; }

		protected BaseBlowfishCryptoService()
		{
			Clear();
		}

		private void Clear()
		{
			_p = new uint[18];
			_s = new uint[4][];

			for(int i = 0; i < _s.Length; i++)
				_s[i] = new uint[256];
		}

		/// <inheritdoc />
		public abstract byte[] Crypt(byte[] bytes);

		/// <inheritdoc />
		public abstract byte[] Crypt(byte[] bytes, int offset, int count);

		/// <summary>
		/// Blowfish F
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		protected uint F(uint x)
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
		protected uint UInt32(byte[] data, int offset)
		{
			return (uint)(data[offset + 0] + (data[offset + 1] << 8) + (data[offset + 2] << 16) + (data[offset + 3] << 24));
		}
		/// <summary>
		/// Put uint bytes into byte array
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="value"></param>
		protected void UInt32(byte[] data, int offset, uint value)
		{
			data[offset + 0] = (byte)(value);
			data[offset + 1] = (byte)(value >> 8);
			data[offset + 2] = (byte)(value >> 16);
			data[offset + 3] = (byte)(value >> 24);
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
		/// <inheritdoc />
		public void Initialize(byte[] key)
		{
			Clear();

			if(key == null)
			{
				throw new ArgumentNullException(nameof(key));
			}
			if(key.Length == 0 || key.Length >= 56)
			{
				throw new ArgumentException("Invalid key length, key length must be higher than 0 and less than 56");
			}

			//Init the key with the XORing mentioned in the original Blowfish commit's CTOR
			for(int i1 = 0; i1 < 48; i1 += 3)
			{
				key[i1] ^= 0x19;
				key[i1 + 1] ^= 0x16;
				key[i1 + 2] ^= 0x18;
			}

			Array.Copy(BlowfishTable.p, this._p, 18);
			Array.Copy(BlowfishTable.s[0], this._s[0], 256);
			Array.Copy(BlowfishTable.s[1], this._s[1], 256);
			Array.Copy(BlowfishTable.s[2], this._s[2], 256);
			Array.Copy(BlowfishTable.s[3], this._s[3], 256);

			// This xoring is not part of the standard blowfish.
			for(int i1 = 0; i1 < 18; i1++)
			{
				ushort pT = (ushort)this._p[i1];
				pT = (ushort)(((pT & 0x00FF) << 8) + ((pT & 0xFF00) >> 8));
				this._p[i1] = (((BlowfishTable.p[i1] >> 16) ^ pT) << 16) + pT;
			}

			for(int i1 = 0; i1 < 18; i1++)
			{
				uint k = (uint)
				((key[(i1 * 4 + 3) % key.Length]) +
					((key[(i1 * 4 + 2) % key.Length]) << 8) +
					((key[(i1 * 4 + 1) % key.Length]) << 16) +
					((key[(i1 * 4 + 0) % key.Length]) << 24));
				this._p[i1] ^= k;
			}

			uint v1 = 0, v2 = 0;
			for(int i1 = 0; i1 < 18; i1 += 2)
			{
				InitBlock(ref v1, ref v2);
				this._p[i1] = v1;
				this._p[i1 + 1] = v2;
			}

			for(int i1 = 0; i1 < 4; i1++)
			{
				for(int i2 = 0; i2 < 256; i2 += 2)
				{
					InitBlock(ref v1, ref v2);
					this._s[i1][i2] = v1;
					this._s[i1][i2 + 1] = v2;
				}
			}

			isInitialized = true;
		}

		/// <inheritdoc />
		public void Uninitialize()
		{
			isInitialized = false;
			Clear();
		}

		private void InitBlock(ref uint L, ref uint R)
		{
			for(int i1 = 0; i1 < 16; i1 += 2)
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

	}
}
