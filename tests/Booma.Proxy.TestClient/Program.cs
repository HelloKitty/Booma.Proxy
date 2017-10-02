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
	/*class Program
	{
		public static ICryptoKeyInitializable<uint> EncryptionKeyInitializer { get; private set; }

		public static ICryptoKeyInitializable<uint> DecryptionKeyInitializer { get; private set; }

		public static IClientMessageContextFactory MessageContextFactory { get; private set; }

		public static void Main(string[] args)
		{
			RunClient("158.69.215.131", 12000).Wait();
		}

		private static async Task RunClient(string ip, int port)
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
			IManagedNetworkClient<PSOBBPatchPacketPayloadClient, PSOBBPatchPacketPayloadServer> client = new PSOBBNetworkClient()
				.AddCryptHandling(encrypt, decrypt)
				.AddHeaderReading(serializer)
				.AddNetworkMessageReading(serializer)
				.For<PSOBBPatchPacketPayloadServer, PSOBBPatchPacketPayloadClient>()
				.AsManaged();

			MessageContextFactory = new DefaultMessageContextFactory();

			await Task.Run(() => RunClientAsync(client, ip, port));
		}

		public static async Task RunClientAsync(IManagedNetworkClient<PSOBBPatchPacketPayloadClient, PSOBBPatchPacketPayloadServer> client, string ip, int port)
		{
			await client.ConnectAsync(ip, port);

			while(true)
			{
				PSOBBNetworkIncomingMessage<PSOBBPatchPacketPayloadServer> message = await client.ReadMessageAsync();
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

		private static async Task HandlePayload(PatchingRedirectPayload redirect, IConnectable client)
		{
			Console.WriteLine($"Redirect to: {new IPAddress(redirect.IPAddress).ToString()}:{redirect.Port}");

			//uninit the crypto
			DecryptionKeyInitializer.Uninitialize();
			EncryptionKeyInitializer.Uninitialize();

			await client.ConnectAsync(new IPAddress(redirect.IPAddress).ToString(), redirect.Port);
		}

		private static async Task HandlePayload(PatchingMessagePayload message, object client)
		{
			Console.WriteLine(message.Message);
		}

		private static async Task HandlePayload(PatchingReadyForLoginRequestPayload readyForLogin, IManagedNetworkClient<PSOBBPatchPacketPayloadClient, PSOBBPatchPacketPayloadServer> client)
		{
			await client.SendMessage(new PatchingLoginRequestPayload("glader", "derp"));
		}

		private static async Task HandlePayload(PatchingWelcomePayload welcome, IManagedNetworkClient<PSOBBPatchPacketPayloadClient, PSOBBPatchPacketPayloadServer> client)
		{
			Console.WriteLine($"Server IV: {welcome.ServerVector}");
			Console.WriteLine($"Client IV: {welcome.ClientVector}");
			Console.WriteLine(welcome.PatchCopyrightMessage);

			//Init the crypto
			EncryptionKeyInitializer.Initialize(welcome.ClientVector);
			DecryptionKeyInitializer.Initialize(welcome.ServerVector);

			//Send the ack to the server
			await client.SendMessage(new PatchingWelcomeAckPayload());
		}

		private static Task HandlePayload(UnknownPatchPacket patchPayload, object o)
		{
			Console.WriteLine($"Encounted {patchPayload.ToString()}");
			return Task.CompletedTask;
		}

		private static Task HandlePayload(object o, object o2)
		{
			return Task.CompletedTask;
		}

		public static void LogMessage(PSOBBNetworkIncomingMessage<PSOBBPatchPacketPayloadServer> message)
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
	}*/
}
