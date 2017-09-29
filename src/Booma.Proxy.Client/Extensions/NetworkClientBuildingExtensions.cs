using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	public static class NetworkClientBuildingExtensions
	{
		/// <summary>
		/// Creates a managed client adapter around the provided <see cref="client"/> providing a high level API
		/// for consumption based on this simplified slimed down <see cref="IManagedNetworkClient{TPayloadWriteType,TPayloadReadType}"/>
		/// interface.
		/// </summary>
		/// <typeparam name="TReadPayloadBaseType">The read type payload (inferred)</typeparam>
		/// <typeparam name="TWritePayloadBaseType">The write type payload (inferred)</typeparam>
		/// <param name="client">The client to adapt.</param>
		/// <returns>A new managed client.</returns>
		public static IManagedNetworkClient<TWritePayloadBaseType, TReadPayloadBaseType> AsManaged<TReadPayloadBaseType, TWritePayloadBaseType>([NotNull] this INetworkMessageClient<TReadPayloadBaseType, TWritePayloadBaseType> client) 
			where TWritePayloadBaseType : class 
			where TReadPayloadBaseType : class
		{
			if(client == null) throw new ArgumentNullException(nameof(client));

			//Adapt the provided network client to the managed network client interfaces.
			return new ManagedPSOBBNetworkClient<INetworkMessageClient<TReadPayloadBaseType, TWritePayloadBaseType>, TWritePayloadBaseType, TReadPayloadBaseType>(client);
		}

		/// <summary>
		/// Enables crypt handling for the client.
		/// </summary>
		/// <typeparam name="TNetworkClientType">The client type.</typeparam>
		/// <param name="client">The client to add crypt handling to.</param>
		/// <param name="encryptionService">The service used to encrypt.</param>
		/// <param name="decryptionService">The service used to decrypt.</param>
		/// <returns>A client with crypto handling functionality.</returns>
		public static NetworkClientCryptoDecorator AddCryptHandling<TNetworkClientType>([NotNull] this TNetworkClientType client, [NotNull] ICryptoServiceProvider encryptionService, [NotNull] ICryptoServiceProvider decryptionService)
			where TNetworkClientType : NetworkClientBase
		{
			if(client == null) throw new ArgumentNullException(nameof(client));
			if(encryptionService == null) throw new ArgumentNullException(nameof(encryptionService));
			if(decryptionService == null) throw new ArgumentNullException(nameof(decryptionService));

			return new NetworkClientCryptoDecorator(client, encryptionService, decryptionService, 4); //TODO: Should we just hardcode the blocksize?
		}

		/// <summary>
		/// Enables header reading functionality to a client.
		/// </summary>
		/// <typeparam name="TNetworkClientType">The client type.</typeparam>
		/// <param name="client">The client to add header reading to.</param>
		/// <returns>A client that can read headers from the network.</returns>
		public static NetworkClientPacketHeaderReaderDecorator AddHeaderReading<TNetworkClientType>([NotNull] this TNetworkClientType client, [NotNull] ISerializerService serializer)
			where TNetworkClientType : NetworkClientBase
		{
			if(client == null) throw new ArgumentNullException(nameof(client));
			if(serializer == null) throw new ArgumentNullException(nameof(serializer));

			return new NetworkClientPacketHeaderReaderDecorator(client, serializer);
		}

		/// <summary>
		/// Enables header reading functionality to a client.
		/// </summary>
		/// <typeparam name="TNetworkClientType">The client type.</typeparam>
		/// <param name="client">The client to add header reading to.</param>
		/// <returns>A client that can read headers from the network.</returns>
		public static NetworkMessageClientBuilder<TNetworkClientType> AddNetworkMessageReading<TNetworkClientType>([NotNull] this TNetworkClientType client, [NotNull] ISerializerService serializer)
			where TNetworkClientType : NetworkClientBase, IPacketHeaderReadable
		{
			if(client == null) throw new ArgumentNullException(nameof(client));
			if(serializer == null) throw new ArgumentNullException(nameof(serializer));

			//New netmessage client builder
			return new NetworkMessageClientBuilder<TNetworkClientType>(serializer, client);
		}
	}
}
