using System;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/// <summary>
	/// Builder for a network message client.
	/// </summary>
	/// <typeparam name="TNetworkType">The decorated network client type.</typeparam>
	public sealed class NetworkMessageClientBuilder<TNetworkType>
		where TNetworkType : NetworkClientBase, IPacketHeaderReadable
	{
		/// <summary>
		/// Serializer dependency for the net message client.
		/// </summary>
		public ISerializerService Serializer { get; }

		/// <summary>
		/// The network client to decorate.
		/// </summary>
		public TNetworkType Client { get; }

		public NetworkMessageClientBuilder([NotNull] ISerializerService serializer, [NotNull] TNetworkType client)
		{
			if(serializer == null) throw new ArgumentNullException(nameof(serializer));
			if(client == null) throw new ArgumentNullException(nameof(client));

			Serializer = serializer;
			Client = client;
		}

		//Hack: Use obsolete to warn user.
		/// <summary>
		/// Allows the fallback for not calling <see cref="For{TPayloadType}"/>.
		/// </summary>
		/// <param name="builder">The builder.</param>
		[Obsolete("Did you mean to call For?")]
		public static implicit operator TNetworkType([NotNull] NetworkMessageClientBuilder<TNetworkType> builder)
		{
			if(builder == null) throw new ArgumentNullException(nameof(builder));

			return builder.Client;
		}

		/// <summary>
		/// Creates a <see cref="NetworkClientBase"/> client that can handle read and writing 
		/// the specified generic <typeparamref name="TPayloadType"/>.
		/// </summary>
		/// <typeparam name="TPayloadType">The payload type.</typeparam>
		/// <returns>A network message client.</returns>
		public NetworkClientPacketPayloadReaderWriterDecorator<TNetworkType, TReadPayloadType, TWritePayloadType> For<TReadPayloadType, TWritePayloadType>()
			where TReadPayloadType : class
			where TWritePayloadType : class
		{
			return new NetworkClientPacketPayloadReaderWriterDecorator<TNetworkType, TReadPayloadType, TWritePayloadType>(Client, Serializer);
		}
	}
}