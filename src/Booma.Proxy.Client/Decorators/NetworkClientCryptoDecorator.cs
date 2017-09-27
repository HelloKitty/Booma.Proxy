using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Crypto decroator for the <see cref="PSOBBNetworkClient"/> that extends the <see cref="Read"/> and
	/// <see cref="Write"/> methods with an encryption implementation.
	/// </summary>
	public sealed class NetworkClientCryptoDecorator : NetworkClientBase, IConnectable, IDisconnectable, IDisposable, 
		IBytesWrittable, IBytesReadable
	{
		/// <summary>
		/// The decorated <see cref="NetworkClientBase"/>.
		/// </summary>
		private NetworkClientBase DecoratedClient { get; }

		/// <summary>
		/// The encryption service provider for the connection.
		/// </summary>
		private ICryptoServiceProvider EncryptionServiceProvider { get; }

		/// <summary>
		/// The decryption service provider for the connection.
		/// </summary>
		private ICryptoServiceProvider DecryptionServiceProvider { get; }

		/// <summary>
		/// Creates a new crypto decorator for the <see cref="PSOBBNetworkClient"/>.
		/// Extends the <see cref="Read"/> and <see cref="Write"/> implementations to pass
		/// all bytes through the corresponding incoming and outgoing ciphers.
		/// </summary>
		/// <param name="decoratedClient">The client to decorate.</param>
		/// <param name="encryptionServiceProvider">The encryption service.</param>
		/// <param name="decryptionServiceProvider">The decryption service.</param>
		public NetworkClientCryptoDecorator(PSOBBNetworkClient decoratedClient, ICryptoServiceProvider encryptionServiceProvider, ICryptoServiceProvider decryptionServiceProvider)
		{
			if(decoratedClient == null) throw new ArgumentNullException(nameof(decoratedClient));
			if(encryptionServiceProvider == null) throw new ArgumentNullException(nameof(encryptionServiceProvider));
			if(decryptionServiceProvider == null) throw new ArgumentNullException(nameof(decryptionServiceProvider));

			DecoratedClient = decoratedClient;
			EncryptionServiceProvider = encryptionServiceProvider;
			DecryptionServiceProvider = decryptionServiceProvider;
		}

		/// <inheritdoc />
		public override async Task<bool> ConnectAsync(IPAddress address, int port)
		{
			return await DecoratedClient.ConnectAsync(address, port);
		}

		/// <inheritdoc />
		public override async Task DisconnectAsync(int delay)
		{
			await DecoratedClient.DisconnectAsync(delay);
		}

		/// <inheritdoc />
		public override async Task WriteAsync(byte[] bytes)
		{
			await DecoratedClient.WriteAsync(EncryptionServiceProvider.Crypt(bytes));
		}

		/// <inheritdoc />
		public override async Task<byte[]> ReadAsync(byte[] buffer, int start, int count, int timeoutInMilliseconds)
		{
			return DecryptionServiceProvider.Crypt(await DecoratedClient.ReadAsync(buffer, start, count, timeoutInMilliseconds));
		}
	}
}
