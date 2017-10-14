using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		public override void Register(IServiceRegister register)
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

			register.RegisterTransient<DefaultMessageContextFactory, IClientMessageContextFactory>();

			register.RegisterInstance<IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer>,
				IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer>>(client);

			register.RegisterInstance<IClientPayloadSendService<PSOBBGamePacketPayloadClient>,
				IClientPayloadSendService<PSOBBGamePacketPayloadClient>>(client);

			register.RegisterInstance<IConnectionService, IConnectionService>(client);

			//TODO: We can just trgister type
			register.RegisterInstance<IClientRequestSendService<PSOBBGamePacketPayloadClient>,
				IClientRequestSendService<PSOBBGamePacketPayloadClient>>(new PayloadInterceptMessageSendService<PSOBBGamePacketPayloadClient>(client, client));

			//Also need to register the crypto service associated with the client.
			register.RegisterInstance<IFullCryptoInitializationService<byte[]>, IFullCryptoInitializationService<byte[]>>(new SeperateAggregateCryptoInitializationService<byte[]>(encrypt, decrypt));

		}
	}
}
