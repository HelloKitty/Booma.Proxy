using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Builder;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	//MessageHandlerService<TPayloadWriteType, TPayloadReadType, IProxiedMessageContext<TPayloadReadType, TPayloadWriteType>
	public class PsobbProxyApplicationBase : ProxiedTcpServerApplicationBase<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>
	{
		enum CryptoType
		{
			Encryption,
			Decryption
		}

		//TODO: Technically this proxy only supports 1 session since these are essentially static
		private EncryptionLazyWithoutKeyDecorator<byte[]> ClientEncryptionService { get; set; }

		private EncryptionLazyWithoutKeyDecorator<byte[]> ClientDecryptionService { get; set; }

		private EncryptionLazyWithoutKeyDecorator<byte[]> ServerEncryptionService { get; set; }

		private EncryptionLazyWithoutKeyDecorator<byte[]> ServerDecryptionService { get; set; }

		/// <inheritdoc />
		public PsobbProxyApplicationBase(NetworkAddressInfo listenerAddress, NetworkAddressInfo proxyToEndpointAddress, ILog logger, PayloadHandlerRegisterationModules<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> handlerModulePair, NetworkSerializerServicePair serializers) 
			: base(listenerAddress, proxyToEndpointAddress, logger, handlerModulePair, serializers)
		{
			//Don't init the crypto stuff here because the base will call RegisterDependecies before this ctor executes
		}

		void ResetCrypto()
		{
			ClientEncryptionService.Uninitialize();
			ClientDecryptionService.Uninitialize();

			ServerEncryptionService.Uninitialize();
			ServerDecryptionService.Uninitialize();
		}

		/// <inheritdoc />
		protected override IManagedNetworkServerClient<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient> BuildIncomingSessionManagedClient(NetworkClientBase clientBase, INetworkSerializationService serializeService)
		{
			ResetCrypto();

			Logger.Info($"Client connecting to Proxy App with Listener: {this.ServerAddress.AddressEndpoint}:{this.ServerAddress.Port}");

			IManagedNetworkServerClient<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient> session = clientBase
				.AddCryptHandling(ClientEncryptionService, ClientDecryptionService)
				.AddBufferredWrite(4)
				.AddHeaderReading<PSOBBPacketHeader>(serializeService, 2)
				.AddNetworkMessageReading(serializeService)
				.For<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer, IPacketPayload>(new PSOBBPacketHeaderFactory())
				.AddReadBufferClearing()
				.Build()
				.AsManagedSession();

			return session;
		}

		/// <inheritdoc />
		protected override IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> BuildOutgoingSessionManagedClient(NetworkClientBase clientBase, INetworkSerializationService serializeService)
		{
			//Copied from the test client project
			IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> client = clientBase
				.AddCryptHandling(ServerEncryptionService, ServerDecryptionService)
				.AddBufferredWrite(4)
				.AddHeaderReading<PSOBBPacketHeader>(serializeService, 2)
				.AddNetworkMessageReading(serializeService)
				.For<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, IPacketPayload>(new PSOBBPacketHeaderFactory())
				.AddReadBufferClearing()
				.Build()
				.AsManagedSession();

			return client;
		}

		//TODO: Move this somewhere better, this is just copied around for testing
		private sealed class PSOBBPacketHeaderFactory : IPacketHeaderFactory<IPacketPayload>
		{
			/// <inheritdoc />
			public IPacketHeader Create<TPayloadType>(TPayloadType payload, byte[] serializedPayloadData)
				where TPayloadType : IPacketPayload
			{
				//The packet size is simply the length of the payload plus the header which is 2 bytes
				return new PSOBBPacketHeader(serializedPayloadData.Length + 2);
			}
		}

		/// <inheritdoc />
		protected override ContainerBuilder RegisterHandlerDependencies(ContainerBuilder builder)
		{
			//Client crypto
			//We create the shared block cipher service here.
			ClientEncryptionService = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishEncryptionService encryptionService = new BlowfishEncryptionService();
				encryptionService.Initialize(val.ToArray());
				return new LegacyGladNetCryptoServiceProviderAdapter(encryptionService);
			}, 8);

			ClientDecryptionService = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishDecryptionService decryptionService = new BlowfishDecryptionService();
				decryptionService.Initialize(val.ToArray());
				return new LegacyGladNetCryptoServiceProviderAdapter(decryptionService);
			}, 8);

			//Server crypto
			ServerEncryptionService = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishEncryptionService encryptionService = new BlowfishEncryptionService();
				encryptionService.Initialize(val.ToArray());
				return new LegacyGladNetCryptoServiceProviderAdapter(encryptionService);
			}, 8);

			ServerDecryptionService = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishDecryptionService decryptionService = new BlowfishDecryptionService();
				decryptionService.Initialize(val.ToArray()); //for PROXY PURPOSES ONLY we have to copy so we don't modify the key used by the other end
				return new LegacyGladNetCryptoServiceProviderAdapter(decryptionService);
			}, 8);

			//Register all the crypto providers as crypto initializers
			RegisterCryptoInitializable(builder, ClientEncryptionService, CryptoType.Decryption);
			RegisterCryptoInitializable(builder, ClientDecryptionService, CryptoType.Encryption);
			RegisterCryptoInitializable(builder, ServerEncryptionService, CryptoType.Encryption);
			RegisterCryptoInitializable(builder, ServerDecryptionService, CryptoType.Decryption);


			builder
				.Register<GladNet.IFullCryptoInitializationService<byte[]>>(context =>
				{
					return new ProxiedFullCryptoInitializable(new AggergateCryptoInitializer(context.ResolveKeyed<IEnumerable<GladNet.ICryptoKeyInitializable<byte[]>>>(CryptoType.Encryption)), new AggergateCryptoInitializer(context.ResolveKeyed<IEnumerable<GladNet.ICryptoKeyInitializable<byte[]>>>(CryptoType.Decryption)));
				});

			builder.RegisterInstance(new BinaryPacketWriter("Packets"))
				.AsSelf()
				.SingleInstance()
				.ExternallyOwned();

			return builder;
		}

		private ContainerBuilder RegisterCryptoInitializable([NotNull] ContainerBuilder builder, [NotNull] GladNet.ICryptoKeyInitializable<byte[]> initializable, CryptoType cryptoType)
		{
			if(builder == null) throw new ArgumentNullException(nameof(builder));
			if(initializable == null) throw new ArgumentNullException(nameof(initializable));

			builder
				.RegisterInstance(initializable)
				.Keyed<ICryptoKeyInitializable<byte[]>>(cryptoType)
				.SingleInstance()
				.ExternallyOwned();

			return builder;
		}

		/// <inheritdoc />
		protected override ContainerBuilder RegisterDefaultHandlers(ContainerBuilder builder)
		{
			//TODO: Default handlers for PSOBB
			//The default handlers (Just forwards)
			builder.RegisterType<DefaultServerPayloadHandler>()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<DefaultClientPayloadHandler>()
				.AsImplementedInterfaces()
				.SingleInstance();

			return builder;
		}
	}
}
