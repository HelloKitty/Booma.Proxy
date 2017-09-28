using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Decorator for a cryptoservice that provides lazy
	/// functionality for it. Doesn't crypt until a key is set.
	/// </summary>
	public sealed class PatchEncryptionLazyWithoutKeyDecorator : ICryptoServiceProvider, ICryptoKeyInitializable<uint>
	{
		/// <summary>
		/// The decorated crypto-provider.
		/// </summary>
		public Lazy<ICryptoServiceProvider> CryptoProvider { get; }

		/// <summary>
		/// The initializaiton vector to use for creating the crypto service
		/// </summary>
		private uint InitializationVector { get; }

		/// <inheritdoc />
		public PatchEncryptionLazyWithoutKeyDecorator()
		{
			CryptoProvider = new Lazy<ICryptoServiceProvider>(InitializeCryptoProvider, true);
		}

		private ICryptoServiceProvider InitializeCryptoProvider()
		{
			//Create from the init key. We expect this to be set before the lazy calls this
			return new PatchServerCryptoProvider(PatchEncryptionKeyFactory.Create(InitializationVector));
		}

		/// <inheritdoc />
		public byte[] Crypt(byte[] bytes)
		{
			if(!CryptoProvider.IsValueCreated)
				return bytes;

			return Crypt(bytes, 0, bytes.Length);
		}

		/// <inheritdoc />
		public byte[] Crypt(byte[] bytes, int offset, int count)
		{
			if(!CryptoProvider.IsValueCreated)
				return bytes;

			return CryptoProvider.Value.Crypt(bytes, offset, count);
		}

		/// <inheritdoc />
		public void SetKey(uint key)
		{
			//Once the key is initialized we can create the CryptoProvider
			//by calling value whic creates it
			ICryptoServiceProvider provider = CryptoProvider.Value;
		}
	}
}
