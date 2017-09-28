﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy.TestClient
{
	class Program
	{
		public static ICryptoKeyInitializable<uint> EncryptionKeyInitializer { get; private set; }

		public static ICryptoKeyInitializable<uint> DecryptionKeyInitializer { get; private set; }

		public static void Main(string[] args)
		{
			PatchEncryptionLazyWithoutKeyDecorator encrypt = new PatchEncryptionLazyWithoutKeyDecorator();
			PatchEncryptionLazyWithoutKeyDecorator decrypt = new PatchEncryptionLazyWithoutKeyDecorator();
			EncryptionKeyInitializer = encrypt;
			DecryptionKeyInitializer = decrypt;

			//Create the serializer and register all the needed types
			SerializerService serializer = new SerializerService();
			
			//Registers all the types.
			PacketPatchServerMetadataMarker.SerializableTypes
				.Concat(PacketCommonServerMetadataMarker.SerializableTypes)
				.Concat(PacketLoginServerMetadataMarker.SerializableTypes)
				.ToList().ForEach(t => serializer.RegisterType(t));

			serializer.Compile();

			//Configurs and builds the clients without all the
			//relevant decorators
			INetworkMessageClient<PSOBBPatchPacketPayloadServer, PSOBBPatchPacketPayloadClient> client = new PSOBBNetworkClient()
				.AddCryptHandling(encrypt, decrypt)
				.AddHeaderReading(serializer)
				.AddNetworkMessageReading(serializer)
				.For<PSOBBPatchPacketPayloadServer, PSOBBPatchPacketPayloadClient>();

			Task.Run(() => RunClientAsync(client)).Wait();
		}

		public static async Task RunClientAsync(INetworkMessageClient<PSOBBPatchPacketPayloadServer, PSOBBPatchPacketPayloadClient> client)
		{
			await client.ConnectAsync("[redacted]", 11000);

			while(true)
			{
				PSOBBNetworkIncomingMessage<PSOBBPatchPacketPayloadServer> message = await client.ReadAsync();
				LogMessage(message);

				HandlePayload((dynamic)message.Payload);
			}
		}

		private static void HandlePayload(UnknownPatchPacket patchPayload)
		{
			Console.WriteLine($"Encounted {patchPayload.ToString()}");
		}

		private static void HandlePayload(object o)
		{

		}

		public static void LogMessage(PSOBBNetworkIncomingMessage<PSOBBPatchPacketPayloadServer> message)
		{
			Console.WriteLine($"Size: {message.Header.PacketSize} Type: {message.Payload.ToString()}");
		}
	}
}
