using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
		/// Indicates the size of the block for crypto padding.
		/// </summary>
		private int BlockSize { get; }

		//TODO: Threadsafety
		private byte[] CryptoBuffer { get; }

		/// <summary>
		/// Creates a new crypto decorator for the <see cref="PSOBBNetworkClient"/>.
		/// Extends the <see cref="Read"/> and <see cref="Write"/> implementations to pass
		/// all bytes through the corresponding incoming and outgoing ciphers.
		/// </summary>
		/// <param name="decoratedClient">The client to decorate.</param>
		/// <param name="encryptionServiceProvider">The encryption service.</param>
		/// <param name="decryptionServiceProvider">The decryption service.</param>
		public NetworkClientCryptoDecorator(NetworkClientBase decoratedClient, ICryptoServiceProvider encryptionServiceProvider, ICryptoServiceProvider decryptionServiceProvider, int blockSize)
		{
			if(decoratedClient == null) throw new ArgumentNullException(nameof(decoratedClient));
			if(encryptionServiceProvider == null) throw new ArgumentNullException(nameof(encryptionServiceProvider));
			if(decryptionServiceProvider == null) throw new ArgumentNullException(nameof(decryptionServiceProvider));
			if(blockSize <= 0) throw new ArgumentOutOfRangeException(nameof(blockSize));

			DecoratedClient = decoratedClient;
			EncryptionServiceProvider = encryptionServiceProvider;
			DecryptionServiceProvider = decryptionServiceProvider;
			BlockSize = blockSize;

			//One of the lobby packets is 14,000 bytes. We may even need bigger.
			CryptoBuffer = new byte[30000]; //TODO: Is this size good? Bigger? Smaller?
		}

		/// <inheritdoc />
		public override async Task<bool> ConnectAsync(IPAddress address, int port)
		{
			return await DecoratedClient.ConnectAsync(address, port)
				.ConfigureAwait(false);
		}

		/// <inheritdoc />
		public override async Task DisconnectAsync(int delay)
		{
			await DecoratedClient.DisconnectAsync(delay)
				.ConfigureAwait(false);
		}

		/// <inheritdoc />
		public override async Task<byte[]> ReadAsync(byte[] buffer, int start, int count, int timeoutInMilliseconds)
		{
			if(start < 0) throw new ArgumentOutOfRangeException(nameof(start));
			if(count < 0) throw new ArgumentOutOfRangeException(nameof(count), $"Cannot read less than 0 bytes. Can't read: {count} many bytes");

			//If the above caller requested an invalid count of bytes to read
			//We should try to correct for it and read afew more bytes.
			count = ConvertToBlocksizeCount(count);

			//We throw above if we have an invalid size that can't be decrypted once read.
			//That means callers will need to be careful in what they request to read.
			return DecryptionServiceProvider.Crypt(await DecoratedClient.ReadAsync(buffer, start, count, timeoutInMilliseconds).ConfigureAwait(false), start, count);
		}

		/// <summary>
		/// Reads asyncronously <see cref="count"/> many bytes from the reader.
		/// </summary>
		/// <param name="buffer">The buffer to store the bytes into.</param>
		/// <param name="start">The start position in the buffer to start reading into.</param>
		/// <param name="count">How many bytes to read.</param>
		/// <param name="token">The cancel token to check during the async operation.</param>
		/// <returns>A future for the read bytes.</returns>
		public override async Task<byte[]> ReadAsync(byte[] buffer, int start, int count, CancellationToken token)
		{
			if(start < 0) throw new ArgumentOutOfRangeException(nameof(start));
			if(count < 0) throw new ArgumentOutOfRangeException(nameof(count), $"Cannot read less than 0 bytes. Can't read: {count} many bytes");

			//If the above caller requested an invalid count of bytes to read
			//We should try to correct for it and read afew more bytes.
			count = ConvertToBlocksizeCount(count);

			if(token.IsCancellationRequested)
				return Enumerable.Empty<byte>().ToArray();

			await DecoratedClient.ReadAsync(buffer, start, count, token)
				.ConfigureAwait(false);

			//Check cancel again, we want to fail quick
			if(token.IsCancellationRequested)
				return Enumerable.Empty<byte>().ToArray();

			//We throw above if we have an invalid size that can't be decrypted once read.
			//That means callers will need to be careful in what they request to read.
			return DecryptionServiceProvider.Crypt(buffer, start, count);
		}

		public override async Task WriteAsync(byte[] bytes, int offset, int count)
		{
			if(offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
			if(count < 0) throw new ArgumentOutOfRangeException(nameof(count));

			int blocksizeAdjustedCount = ConvertToBlocksizeCount(count);

			if(count == blocksizeAdjustedCount)
				await DecoratedClient.WriteAsync(EncryptionServiceProvider.Crypt(bytes, offset, count), offset, count)
					.ConfigureAwait(false);
			else
			{
				try
				{
					//We copy to the thread local buffer so we can use it as an extended buffer by "neededBytes" many more bytes.
					//So the buffer is very large but we'll tell it to write bytes.length + neededBytes.
					Buffer.BlockCopy(bytes, offset, CryptoBuffer, 0, count); //dont copy full array, might only need less with count
				}
				catch(Exception e)
				{
					throw new InvalidOperationException($"Failed to copy bytes to crypto buffer. Bytes Length: {bytes.Length} Offset: {offset} Count: {count} BlocksizeAdjustedCount: {blocksizeAdjustedCount}", e);
				}

				EncryptionServiceProvider.Crypt(CryptoBuffer, 0, blocksizeAdjustedCount);

				//recurr to write the bytes with the now properly sized buffer.
				await DecoratedClient.WriteAsync(CryptoBuffer, 0, blocksizeAdjustedCount)
					.ConfigureAwait(false);
			}
		}

		private int ConvertToBlocksizeCount(int count)
		{
			int remainder = count % BlockSize;

			//Important to check if it's already perfectly size
			//otherwise below code will return count + blocksize
			if(remainder == 0)
				return count;

			return count + (BlockSize - (count % BlockSize));
		}
	}
}
