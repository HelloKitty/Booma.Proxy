using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Logging;
using FreecraftCore.Serializer;
using GladNet;
using SceneJect.Common;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Module responsible for registering the dependencies associated with the game client.
	/// </summary>
	public sealed class GameClientDependencyRegisterModule : NonBehaviourDependency
	{
		//For performance reasons we only ever build a single serializer
		private static Lazy<INetworkSerializationService> Serializer { get; } = new Lazy<INetworkSerializationService>(CreateSerializer, true);

		private static INetworkSerializationService CreateSerializer()
		{
			//Create the serializer and register all the needed types
			SerializerService serializer = new SerializerService();

			//Registers all the types.
			PacketCommonServerMetadataMarker.SerializableTypes
				.Concat(PacketLoginServerMetadataMarker.SerializableTypes)
				.Concat(PacketSharedServerMetadataMarker.SerializableTypes)
				.Concat(PacketBlockServerMetadataMarker.SerializableTypes)
				.Concat(PacketShipServerMetadataMarker.SerializableTypes)
				.Concat(PacketCharacterServerMetadataMarker.SerializableTypes)
				.ToList().ForEach(t => serializer.RegisterType(t));

			serializer.Compile();

			return new FreecraftCoreGladNetSerializerAdapter(serializer);
		}

		[SerializeField]
		private LogLevel LoggingLevel;

		/// <inheritdoc />
		public override void Register(ContainerBuilder register)
		{
			EncryptionLazyWithoutKeyDecorator<byte[]> encrypt = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishEncryptionService encryptionService = new BlowfishEncryptionService();
				encryptionService.Initialize(val);
				return encryptionService;
			}, 8);
			EncryptionLazyWithoutKeyDecorator<byte[]> decrypt = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishDecryptionService decryptionService = new BlowfishDecryptionService();
				decryptionService.Initialize(val);
				return decryptionService;
			}, 8);

			IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> client = new DotNetTcpClientNetworkClient()
				.AddCryptHandling(encrypt, decrypt)
				.AddBufferredWrite(4)
				.AddHeaderReading<PSOBBPacketHeader>(Serializer.Value, 2)
				.AddNetworkMessageReading(Serializer.Value)
				.For<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, IPacketPayload>(new PSOBBPacketHeaderFactory())
				.AddReadBufferClearing()
				.Build()
				.AsManaged(new UnityLoggingService(LoggingLevel));

			register.RegisterInstance(client)
				.As<IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer>>()
				.As<IPeerPayloadSendService<PSOBBGamePacketPayloadClient>>()
				.As<IPayloadInterceptable>();

			register.RegisterType<DefaultMessageContextFactory>()
				.As<IPeerMessageContextFactory>()
				.SingleInstance();

			register.RegisterType<PayloadInterceptMessageSendService<PSOBBGamePacketPayloadClient>>()
				.As<IPeerRequestSendService<PSOBBGamePacketPayloadClient>>()
				.SingleInstance();

			register.RegisterInstance(new SeperateAggregateCryptoInitializationService<byte[]>(encrypt, decrypt))
				.As<IFullCryptoInitializationService<byte[]>>()
				.SingleInstance();

			//TODO: This is all so hacky, is this really what we're going to do forever??
			register.RegisterType<GlobalExportableClient>()
				.As<INetworkClientExportable>()
				.SingleInstance();

			register.RegisterType<DefaultConnectionRedirector>()
				.As<IConnectionRedirector>();

			register.RegisterType<GlobalConnectionService>()
				.As<IConnectionService>()
				.SingleInstance();
		}
	}
}
