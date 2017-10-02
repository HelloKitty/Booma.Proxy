using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy.TestClient
{
	class Program
	{
		public static ICryptoKeyInitializable<byte[]> EncryptionKeyInitializer { get; private set; }

		public static ICryptoKeyInitializable<byte[]> DecryptionKeyInitializer { get; private set; }

		public static IClientMessageContextFactory MessageContextFactory { get; private set; }

		public static void Main(string[] args)
		{
			RunClient("158.69.215.131", 12000).Wait();
		}

		private static async Task RunClient(string ip, int port)
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
			IManagedNetworkClient<PSOBBLoginPacketPayloadClient, PSOBBLoginPacketPayloadServer> client = new PSOBBNetworkClient()
				.AddCryptHandling(encrypt, decrypt, 8)
				.AddHeaderReading(serializer, 8)
				.AddNetworkMessageReading(serializer)
				.For<PSOBBLoginPacketPayloadServer, PSOBBLoginPacketPayloadClient>()
				.AsManaged();

			MessageContextFactory = new DefaultMessageContextFactory();

			await Task.Run(() => RunClientAsync(client, ip, port));
		}

		public static async Task RunClientAsync(IManagedNetworkClient<PSOBBLoginPacketPayloadClient, PSOBBLoginPacketPayloadServer> client, string ip, int port)
		{
			await client.ConnectAsync(ip, port);

			while(true)
			{
				PSOBBNetworkIncomingMessage<PSOBBLoginPacketPayloadServer> message = await client.ReadMessageAsync();

				LogMessage(message);

				try
				{
					//IClientMessageContext<PSOBBPatchPacketPayloadClient> context = MessageContextFactory.Create(client, client);

					await HandlePayload((dynamic)message.Payload, client);


					//await p.TryHandleMessage(context, message);

				}
				catch(Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}
		}

		private static async Task HandlePayload(LoginWelcomePayload payload, IManagedNetworkClient<PSOBBLoginPacketPayloadClient, PSOBBLoginPacketPayloadServer> client)
		{
			Console.WriteLine($"Server Vector: {payload.ServerVector.Aggregate("", (s, b) => $"{s} {b.ToString()}")}");
			Console.WriteLine($"Client Vector: {payload.ClientVector.Aggregate("", (s, b) => $"{s} {b.ToString()}")}");

			EncryptionKeyInitializer.Initialize(payload.ClientVector);
			DecryptionKeyInitializer.Initialize(payload.ServerVector);

			Console.WriteLine(payload.CopyrightMessage);

			//await client.SendMessage(new LoginWelcomeAckPayload());
			await client.SendMessage(new LoginLoginRequest93Payload(0x41, "glader", "playpso69", new ClientVerificationData(0x41, new byte[40])));
		}

		public static void LogMessage(PSOBBNetworkIncomingMessage<PSOBBLoginPacketPayloadServer> message)
		{
			if(message.Payload is IUnknownPayloadType unk)
			{
				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.WriteLine($"Size: {message.Header.PacketSize} OpCode: 0x{unk.OperationCode:X} Type: {message.Payload.GetType().Name}");
				Console.BackgroundColor = ConsoleColor.Black;
			}
			else
			{
				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.WriteLine($"Size: {message.Header.PacketSize} OpCode: 0x{message.Payload.GetType().GetTypeInfo().GetCustomAttribute<WireDataContractBaseLinkAttribute>().Index:X} Type: {message.Payload.GetType().Name}");
				Console.BackgroundColor = ConsoleColor.Black;
			}
		}
	}
}
