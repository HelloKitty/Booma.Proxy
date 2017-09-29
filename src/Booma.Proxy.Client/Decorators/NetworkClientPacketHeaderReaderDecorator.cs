using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using System.Threading;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/// <summary>
	/// Decorator that adds header reading functionality to the <see cref="NetworkClientBase"/>.
	/// </summary>
	public sealed class NetworkClientPacketHeaderReaderDecorator : NetworkClientBase, IPacketHeaderReadable
	{
		/// <summary>
		/// The decorated <see cref="NetworkClientBase"/>.
		/// </summary>
		private NetworkClientBase DecoratedClient { get; }

		/// <summary>
		/// The serialization service.
		/// </summary>
		private ISerializerService Serializer { get; }

		//TODO: Thread safety
		/// <summary>
		/// Thread specific buffer used to deserialize the packet header bytes into.
		/// </summary>
		private byte[] PacketHeaderBuffer { get; }

		//TODO: Thread safety
		/// <summary>
		/// Indicates if we have buffed bytes that were
		/// originally for the header (the opcode) and if
		/// we don't need to worry about them.
		/// If this is false then we do have header bytes and need to handle them
		/// before reading further.
		/// </summary>
		private bool isHeaderFullyRead { get; set; } = true;

		/// <summary>
		/// </summary>
		/// <param name="decoratedClient">The client to decorate.</param>
		public NetworkClientPacketHeaderReaderDecorator(NetworkClientBase decoratedClient, [NotNull] ISerializerService serializer)
		{
			if(decoratedClient == null) throw new ArgumentNullException(nameof(decoratedClient));
			if(serializer == null) throw new ArgumentNullException(nameof(serializer));

			PacketHeaderBuffer = new byte[4];
			DecoratedClient = decoratedClient;
			Serializer = serializer;
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
		public override async Task WriteAsync(byte[] bytes, int offset, int count)
		{
			await DecoratedClient.WriteAsync(bytes, offset, count);
		}

		/// <inheritdoc />
		public override async Task<byte[]> ReadAsync(byte[] buffer, int start, int count, int timeoutInMilliseconds)
		{
			//Check if we have leftover header bytes
			if(isHeaderFullyRead)
				return await DecoratedClient.ReadAsync(buffer, start, count, timeoutInMilliseconds);

			buffer[start] = PacketHeaderBuffer[2];
			buffer[start + 1] = PacketHeaderBuffer[3];
			isHeaderFullyRead = true;
			
			//If we only wanted 2 bytes then we need to get out now
			if(count == 2)
				return buffer;

			//Since we inserted the remaining buffered header bytes into the buffer the caller wants to read into
			//then we should offset by 2 and read 2 less bytes
			return await DecoratedClient.ReadAsync(buffer, start + 2, count - 2, timeoutInMilliseconds);
		}

		//TODO: This is copy-pasted from above, to avoid creating tokens when we don't need them. Should we refactor?
		public override async Task<byte[]> ReadAsync(byte[] buffer, int start, int count, CancellationToken token)
		{
			//Check if we have leftover header bytes
			if(isHeaderFullyRead)
				return await DecoratedClient.ReadAsync(buffer, start, count, token);

			buffer[start] = PacketHeaderBuffer[2];
			buffer[start + 1] = PacketHeaderBuffer[3];
			isHeaderFullyRead = true;

			//If we only wanted 2 bytes then we need to get out now
			if(count == 2)
				return buffer;

			//Since we inserted the remaining buffered header bytes into the buffer the caller wants to read into
			//then we should offset by 2 and read 2 less bytes
			return await DecoratedClient.ReadAsync(buffer, start + 2, count - 2, token);
		}

		/// <inheritdoc />
		public IPacketHeader ReadHeader()
		{
			return ReadHeaderAsync().Result;
		}

		/// <inheritdoc />
		public async Task<IPacketHeader> ReadHeaderAsync()
		{
			if(!isHeaderFullyRead)
				throw new InvalidOperationException("Cannot read any more headers until the buffered bytes in the header buffer has been read.");

			//The header we know is 4 bytes.
			//If we had access to the stream we could wrap it in a reader and use it
			//without knowing the size. Since we don't have access we must manually read
			await ReadAsync(PacketHeaderBuffer, 0, 4, 0); //TODO: How long should the timeout be if any?

			//Since we only deserialize with 2 bytes the header is not fully read
			//meaning 2 bytes including the opcode will be left in the buffer
			//that need to be read before any need data.
			isHeaderFullyRead = false;

			//This will deserialize
			return Serializer.Deserialize<PSOBBPacketHeader>(PacketHeaderBuffer);
		}

		public async Task<IPacketHeader> ReadHeaderAsync(CancellationToken token)
		{
			if(!isHeaderFullyRead)
				throw new InvalidOperationException("Cannot read any more headers until the buffered bytes in the header buffer has been read.");

			//If the token is canceled just return null;
			if(token.IsCancellationRequested)
				return null;

			//The header we know is 4 bytes.
			//If we had access to the stream we could wrap it in a reader and use it
			//without knowing the size. Since we don't have access we must manually read
			await ReadAsync(PacketHeaderBuffer, 0, 4, token); //TODO: How long should the timeout be if any?

			//If the token is canceled just return null;
			if(token.IsCancellationRequested)
				return null;

			//Since we only deserialize with 2 bytes the header is not fully read
			//meaning 2 bytes including the opcode will be left in the buffer
			//that need to be read before any need data.
			isHeaderFullyRead = false;

			//This will deserialize
			return Serializer.Deserialize<PSOBBPacketHeader>(PacketHeaderBuffer);
		}
	}
}
