using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Logging;
using FreecraftCore.Serializer;
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
		private static Lazy<ISerializerService> Serializer { get; } = new Lazy<ISerializerService>(CreateSerializer, true);

		private static ISerializerService CreateSerializer()
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

			return serializer;
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
			});
			EncryptionLazyWithoutKeyDecorator<byte[]> decrypt = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishDecryptionService decryptionService = new BlowfishDecryptionService();
				decryptionService.Initialize(val);
				return decryptionService;
			});

			IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> client = new PSOBBNetworkClient()
				.AddCryptHandling(encrypt, decrypt, 8)
				.AddHeaderReading(Serializer.Value, 8)
				.AddNetworkMessageReading(Serializer.Value)
				.For<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>()
				.AsManaged(new UnityLoggingService(LoggingLevel));

			register.RegisterInstance(client)
				.As<IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer>>()
				.As<IClientPayloadSendService<PSOBBGamePacketPayloadClient>>()
				.As<IPayloadInterceptable>()
				.As<IConnectionService>();

			register.RegisterType<DefaultMessageContextFactory>()
				.As<IClientMessageContextFactory>()
				.SingleInstance();

			register.RegisterType<PayloadInterceptMessageSendService<PSOBBGamePacketPayloadClient>>()
				.As<IClientRequestSendService<PSOBBGamePacketPayloadClient>>()
				.SingleInstance();

			register.RegisterInstance(new SeperateAggregateCryptoInitializationService<byte[]>(encrypt, decrypt))
				.As<IFullCryptoInitializationService<byte[]>>()
				.SingleInstance();
		}
	}
}
