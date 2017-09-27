using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using System.Threading;

namespace Booma.Proxy
{
	/// <summary>
	/// Decorator that adds header reading functionality to the <see cref="NetworkClientBase"/>.
	/// </summary>
	public sealed class NetworkClientPacketReaderDecorator : NetworkClientBase, IPacketHeaderReadable
	{
		/// <summary>
		/// The decorated <see cref="NetworkClientBase"/>.
		/// </summary>
		private NetworkClientBase DecoratedClient { get; }

		/// <summary>
		/// The serialization service.
		/// </summary>
		private ISerializerService Serializer { get; }

		/// <summary>
		/// Thread specific buffer used to deserialize the packet header bytes into.
		/// </summary>
		private ThreadLocal<byte[]> PacketHeaderBuffer { get; }

		/// <summary>
		/// </summary>
		/// <param name="decoratedClient">The client to decorate.</param>
		public NetworkClientPacketReaderDecorator(NetworkClientBase decoratedClient)
		{
			if(decoratedClient == null) throw new ArgumentNullException(nameof(decoratedClient));

			PacketHeaderBuffer = new ThreadLocal<byte[]>(() => new byte[2]);
			DecoratedClient = decoratedClient;
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
			await DecoratedClient.WriteAsync(bytes);
		}

		/// <inheritdoc />
		public override async Task<byte[]> ReadAsync(byte[] buffer, int start, int count, int timeoutInMilliseconds)
		{
			return await DecoratedClient.ReadAsync(buffer, start, count, timeoutInMilliseconds);
		}

		/// <inheritdoc />
		public IPacketHeader ReadHeader()
		{
			return ReadHeaderAsync().Result;
		}

		/// <inheritdoc />
		public async Task<IPacketHeader> ReadHeaderAsync()
		{
			//The header we know is two bytes.
			//If we had access to the stream we could wrap it in a reader and use it
			//without knowing the size. Since we don't have access we must manually read
			await ReadAsync(PacketHeaderBuffer.Value, 0, 2, 0); //TODO: How long should the timeout be if any?

			return Serializer.Deserialize<PSOBBPacketHeader>(PacketHeaderBuffer.Value);
		}

		/// <inheritdoc />
		protected override void Dispose(bool disposing)
		{
			PacketHeaderBuffer.Dispose();
			base.Dispose(disposing);
		}
	}
}
