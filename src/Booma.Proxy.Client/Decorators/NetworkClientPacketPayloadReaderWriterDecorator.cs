using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/// <summary>
	/// Decorator that decorates the provided <see cref="NetworkClientBase"/> with functionality
	/// that allows you to write <see cref="TPayloadBaseType"/> directly into the stream/client.
	/// Overloads the usage of <see cref="Write"/> to accomplish this.
	/// </summary>
	/// <typeparam name="TClientType">The type of decorated client.</typeparam>
	/// <typeparam name="TPayloadBaseType">The payload base type.</typeparam>
	public sealed class NetworkClientPacketPayloadReaderWriterDecorator<TClientType, TPayloadBaseType> : NetworkClientBase, 
		IPacketPayloadWritable<TPayloadBaseType>, IPacketPayloadReadable<TPayloadBaseType>
		where TClientType : NetworkClientBase, IPacketHeaderReadable
		where TPayloadBaseType : class
	{
		/// <summary>
		/// The decorated client.
		/// </summary>
		private TClientType DecoratedClient { get; }

		/// <summary>
		/// The serializer service.
		/// </summary>
		private ISerializerService Serializer { get; }

		/// <summary>
		/// Thread specific buffer used to deserialize the packet header bytes into.
		/// </summary>
		private ThreadLocal<byte[]> PacketPayloadBuffer { get; }

		public NetworkClientPacketPayloadReaderWriterDecorator([NotNull] TClientType decoratedClient, [NotNull] ISerializerService serializer)
		{
			if(decoratedClient == null) throw new ArgumentNullException(nameof(decoratedClient));
			if(Serializer == null) throw new ArgumentNullException(nameof(Serializer));

			DecoratedClient = decoratedClient;
			Serializer = serializer;
			PacketPayloadBuffer = new ThreadLocal<byte[]>(() => new byte[500]); //TODO: Do we need a larger buffer for any packets?
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

		public void Write(TPayloadBaseType payload)
		{
			//Write the outgoing message, it will internally create the header and it will be serialized
			WriteAsync(payload).Wait();
		}

		public async Task WriteAsync(TPayloadBaseType payload)
		{
			//Write the outgoing message, it will internally create the header and it will be serialized
			await DecoratedClient.WriteAsync(Serializer.Serialize(new PSOBBNetworkOutgoingMessage(Serializer.Serialize(payload))));
		}

		/// <inheritdoc />
		public PSOBBNetworkIncomdingMessage<TPayloadBaseType> Read()
		{
			return ReadAsync().Result;
		}

		/// <inheritdoc />
		public async Task<PSOBBNetworkIncomdingMessage<TPayloadBaseType>> ReadAsync()
		{
			//Read the header first
			IPacketHeader header = DecoratedClient.ReadHeader();

			//We need to read enough bytes to deserialize the payload
			await ReadAsync(PacketPayloadBuffer.Value, 0, header.PayloadSize, 0); //TODO: Should we timeout?

			TPayloadBaseType payload = Serializer.Deserialize<TPayloadBaseType>(new FixedBufferWireReaderStrategy(PacketPayloadBuffer.Value, header.PayloadSize));

			return new PSOBBNetworkIncomdingMessage<TPayloadBaseType>(header, payload);
		}
	}
}
