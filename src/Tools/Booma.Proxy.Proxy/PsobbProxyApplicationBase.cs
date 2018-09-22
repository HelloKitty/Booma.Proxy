using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Builder;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	public class PsobbProxyApplicationBase : ProxiedTcpServerApplicationBase<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>
	{
		//TODO: Technically this proxy only supports 1 session since these are essentially static
		private EncryptionLazyWithoutKeyDecorator<byte[]> ClientEncryptionService { get; }

		private EncryptionLazyWithoutKeyDecorator<byte[]> ClientDecryptionService { get; }

		private EncryptionLazyWithoutKeyDecorator<byte[]> ServerEncryptionService { get; }

		private EncryptionLazyWithoutKeyDecorator<byte[]> ServerDecryptionService { get; }

		/// <inheritdoc />
		public PsobbProxyApplicationBase(NetworkAddressInfo listenerAddress, NetworkAddressInfo proxyToEndpointAddress, ILog logger, PayloadHandlerRegisterationModules<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> handlerModulePair, NetworkSerializerServicePair serializers) 
			: base(listenerAddress, proxyToEndpointAddress, logger, handlerModulePair, serializers)
		{
			//Client crypto
			//We create the shared block cipher service here.
			ClientEncryptionService = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishEncryptionService encryptionService = new BlowfishEncryptionService();
				encryptionService.Initialize(val);
				return encryptionService;
			}, 8);

			ClientDecryptionService = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishDecryptionService decryptionService = new BlowfishDecryptionService();
				decryptionService.Initialize(val);
				return decryptionService;
			}, 8);

			//Server crypto
			ServerEncryptionService = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishEncryptionService encryptionService = new BlowfishEncryptionService();
				encryptionService.Initialize(val);
				return encryptionService;
			}, 8);

			ServerDecryptionService = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishDecryptionService decryptionService = new BlowfishDecryptionService();
				decryptionService.Initialize(val);
				return decryptionService;
			}, 8);
		}

		/// <inheritdoc />
		protected override IManagedNetworkServerClient<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient> BuildIncomingSessionManagedClient(NetworkClientBase clientBase, INetworkSerializationService serializeService)
		{
			IManagedNetworkServerClient<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient> session = new DotNetTcpClientNetworkClient()
				.AddCryptHandling(ServerEncryptionService, ServerDecryptionService)
				.AddHeaderReading<PSOBBPacketHeader>(serializeService, 4)
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
			IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> client = new DotNetTcpClientNetworkClient()
				.AddCryptHandling(ClientEncryptionService, ClientDecryptionService)
				.AddHeaderReading<PSOBBPacketHeader>(serializeService, 4)
				.AddNetworkMessageReading(serializeService)
				.For<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, IPacketPayload>(new PSOBBPacketHeaderFactory())
				.AddReadBufferClearing()
				.Build()
				.AsManaged();

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
			//Register all the crypto providers as crypto initializers
			RegisterCryptoInitializable(builder, ClientEncryptionService);
			RegisterCryptoInitializable(builder, ClientDecryptionService);
			RegisterCryptoInitializable(builder, ServerEncryptionService);
			RegisterCryptoInitializable(builder, ServerDecryptionService);

			return builder;
		}

		private ContainerBuilder RegisterCryptoInitializable(ContainerBuilder builder, ICryptoKeyInitializable<byte[]> initializable)
		{
			builder
				.RegisterInstance(initializable)
				.As<ICryptoKeyInitializable<byte[]>>()
				.SingleInstance()
				.ExternallyOwned();

			return builder;
		}

		/// <inheritdoc />
		protected override ContainerBuilder RegisterDefaultHandlers(ContainerBuilder builder)
		{
			//TODO: Default handlers for PSOBB
			//The default handlers (Just forwards)
			/*builder.RegisterType<AuthDefaultServerResponseHandler>()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<AuthDefaultClientRequestHandler>()
				.AsImplementedInterfaces()
				.SingleInstance();*/

			return builder;
		}
	}
}
